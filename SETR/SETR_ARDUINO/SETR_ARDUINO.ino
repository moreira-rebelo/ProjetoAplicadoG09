/*
  Ethernet Modbus TCP Server LED

  This sketch creates a Modbus TCP Server with a simulated coil.
  The value of the simulated coil is set on the LED

  Circuit:
   - Any Arduino MKR Board
   - MKR ETH Shield

  created 16 July 2018
  by Sandeep Mistry
*/

#include <SPI.h>
#include <Ethernet.h>
#include <ArduinoRS485.h> // ArduinoModbus depends on the ArduinoRS485 library
#include <ArduinoModbus.h>
#include "DHT.h"
#include <MFRC522.h>

// Enter a MAC address for your controller below.
// Newer Ethernet shields have a MAC address printed on a sticker on the shield
// The IP address will be dependent on your local network:
byte mac[] = {
  0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED
};
IPAddress ip(192, 168, 1, 15);

EthernetServer ethServer(502);

#define DHTPIN 2
#define DHTTYPE DHT11
#define SS_PIN 5
#define RST_PIN 9

ModbusTCPServer modbusTCPServer;
DHT dht(DHTPIN, DHTTYPE);

MFRC522 rfid(SS_PIN, RST_PIN); // Instance of the class

MFRC522::MIFARE_Key key; 

// Init array that will store new NUID 
byte nuidPICC[4];


void setup() {
  // You can use Ethernet.init(pin) to configure the CS pin
  //Ethernet.init(10);  // Most Arduino shields
  //Ethernet.init(5);   // MKR ETH shield
  //Ethernet.init(0);   // Teensy 2.0
  //Ethernet.init(20);  // Teensy++ 2.0
  //Ethernet.init(15);  // ESP8266 with Adafruit Featherwing Ethernet
  //Ethernet.init(33);  // ESP32 with Adafruit Featherwing Ethernet

  // Open serial communications and wait for port to open:
  Serial.begin(9600);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }
  Serial.println("Ethernet Modbus TCP Example");

  // start the Ethernet connection and the server:
  Ethernet.begin(mac, ip);

  // Check for Ethernet hardware present
  if (Ethernet.hardwareStatus() == EthernetNoHardware) {
    Serial.println("Ethernet shield was not found.  Sorry, can't run without hardware. :(");
    while (true) {
      delay(1); // do nothing, no point running without Ethernet hardware
    }
  }
  if (Ethernet.linkStatus() == LinkOFF) {
    Serial.println("Ethernet cable is not connected.");
  }

  // start the server
  ethServer.begin();
  
  // start the Modbus TCP server
  if (!modbusTCPServer.begin()) {
    Serial.println("Failed to start Modbus TCP Server!");
    while (1);
  }

  // configure the LED
  pinMode(9, OUTPUT);
  digitalWrite(9, LOW);

  // configure a single coil at address 0x00
  modbusTCPServer.configureCoils(0x1, 1);

  dht.begin();
  SPI.begin(); // Init SPI bus
  rfid.PCD_Init(); // Init MFRC522 
  for (byte i = 0; i < 6; i++) {
    key.keyByte[i] = 0xFF;
  }

}

void loop() {
  // listen for incoming clients
  EthernetClient client = ethServer.available();

  //readDht();
  rfidRead();
  
  if (client) {
    // a new client connected
    Serial.println("new client");

    // let the Modbus TCP accept the connection 
    modbusTCPServer.accept(client);

    while (client.connected()) {
      // poll for Modbus TCP requests, while client connected
      modbusTCPServer.poll();

      // update the LED
      updateLED();

      
    }

    Serial.println("client disconnected");
  }
}

void updateLED() {
  // read the current value of the coil
  int coilValue = modbusTCPServer.coilRead(0x1);

  if (coilValue) {
    // coil value set, turn LED on
    digitalWrite(9, HIGH);
    Serial.println("LED ON");
  } else {
    // coild value clear, turn LED off
    digitalWrite(9, LOW);
    Serial.println("LED OFF");
  }
}

void readDht() {
  // Wait a few seconds between measurements.
  delay(2000);

  // Reading temperature or humidity takes about 250 milliseconds!
  // Sensor readings may also be up to 2 seconds 'old' (its a very slow sensor)
  float h = dht.readHumidity();
  // Read temperature as Celsius (the default)
  float t = dht.readTemperature();

  // Check if any reads failed and exit early (to try again).
  if (isnan(h) || isnan(t)) {
    Serial.println(F("Failed to read from DHT sensor!"));
    return;
  }

  // Compute heat index in Celsius (isFahreheit = false)
  float hic = dht.computeHeatIndex(t, h, false);

  Serial.print(F("Humidity: "));
  Serial.print(h);
  Serial.print(F("%  Temperature: "));
  Serial.print(t);
  Serial.print(F("°C "));
  Serial.print(hic);
  Serial.print(F("°C "));
  Serial.print("\n");
  
}

void rfidRead() {

  // Look for new cards
  if ( ! rfid.PICC_IsNewCardPresent())
    return;

  // Verify if the NUID has been readed
  if ( ! rfid.PICC_ReadCardSerial())
    return;



 for (byte i = 0; i < 4; i++) {
      nuidPICC[i] = rfid.uid.uidByte[i];
    }
   
  printHex(rfid.uid.uidByte, rfid.uid.size);
    Serial.println();
   rfid.PICC_HaltA();

  
  rfid.PCD_StopCrypto1();
}



void printHex(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i] < 0x10 ? " 0" : " ");
    Serial.print(buffer[i], HEX);
  }
}


