#include <SPI.h>
#include <Ethernet.h>
#include <MFRC522.h>
#include <MQTT.h>

#include "DHT.h"
#include "pitches.h"

byte mac[] = {0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED};
byte ip[] = {192, 168, 1, 15};

#define DHTPIN 2
#define DHTTYPE DHT11
#define SS_PIN 5
#define RST_PIN 9
#define LED_RED 8
#define LED_GREEN 7
#define LED_BLUE 6
#define BUZZER 4

EthernetClient net;
MQTTClient client;
DHT dht(DHTPIN, DHTTYPE);
MFRC522 rfid(SS_PIN, RST_PIN);
MFRC522::MIFARE_Key key;

byte nuidPICC[4];
unsigned long lastDhtReadTime = 0;
const unsigned long intervalDHT = 60000;
float tempSetpoint;
float tempActual;
String fechaduraStatus = "LOCKED";

void setup() {
    Serial.begin(9600);
    Ethernet.begin(mac, ip);

    client.begin("192.168.1.14", net);
    client.onMessage(messageReceived);  // Set the callback function for MQTT messages

    connect();

    dht.begin();
    SPI.begin();
    rfid.PCD_Init();

    for (byte i = 0; i < 6; i++) {
        key.keyByte[i] = 0xFF;
    }
}

void loop() {
    client.loop();

    if (!client.connected()) {
        connect();
    }

    rfidRead();

    ac(tempSetpoint, tempActual);
    fechadura(fechaduraStatus);

    readDht();

    if (millis() - lastDhtReadTime >= intervalDHT) {
        readDht();
        lastDhtReadTime = millis();
    }
}

void readDht() {
    tempActual = dht.readTemperature();

    String temperatureString = String(tempActual);
    client.publish("actualTemp", temperatureString.c_str());
}

void rfidRead() {
    if (!rfid.PICC_IsNewCardPresent()) return;

    if (!rfid.PICC_ReadCardSerial()) return;

    for (byte i = 0; i < 4; i++) {
        nuidPICC[i] = rfid.uid.uidByte[i];
    }

    digitalWrite(LED_GREEN, HIGH);
    tone(BUZZER, NOTE_C5, 100);
    delay(50);
    tone(BUZZER, NOTE_G5, 100);
    delay(50);
    tone(BUZZER, NOTE_C6, 100);
    digitalWrite(LED_GREEN, LOW);

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

void connect() {
    Serial.print("connecting...");
    while (!client.connect("arduino", "public", "public")) {
        Serial.print(".");
        delay(1000);
    }
    Serial.println("\nconnected!");

    client.subscribe("fechaduraActuator");
    client.subscribe("tempSetpoint");
}

void messageReceived(String &topic, String &payload) {
    if (topic == "fechaduraActuator" && payload == "ON") {
        fechaduraStatus = "UNLOCKED";
        digitalWrite(LED_GREEN, HIGH);
        tone(BUZZER, NOTE_C5, 100);
        delay(50);
        tone(BUZZER, NOTE_G5, 100);
        delay(50);
        tone(BUZZER, NOTE_C6, 100);
        delay(1000);
        digitalWrite(LED_GREEN, LOW);
    }

    if (topic == "tempSetpoint") {
        tempSetpoint = payload.toFloat(); // Convert the payload to a float and assign it to tempSetpoint
        Serial.print("Received Temperature Setpoint: ");
        Serial.println(payload); // Print the received temperature setpoint to the Serial Monitor
    }

}

void ac(float tempSetpoint, float tempActual) {
    if (tempActual < tempSetpoint) {
        digitalWrite(LED_RED, HIGH);
        digitalWrite(LED_GREEN, LOW);
        digitalWrite(LED_BLUE, LOW);
        //Serial.println("Aquecer");
        client.publish("acStatus", "Heating");
    }
    else if (tempActual > tempSetpoint){
        digitalWrite(LED_RED, LOW);
        digitalWrite(LED_GREEN, LOW);
        digitalWrite(LED_BLUE, HIGH);
        //Serial.println("Arrefecer");
        client.publish("acStatus", "Cooling");
    }
    else if (tempActual = tempSetpoint){
        digitalWrite(LED_RED, LOW);
        digitalWrite(LED_GREEN, LOW);
        digitalWrite(LED_BLUE, LOW);
        //Serial.println("Desligado");
        client.publish("acStatus", "OFF");
    }
}

void fechadura(String &fechaduraStatus){
  if (fechaduraStatus == "LOCKED"){
    //Serial.println("Locked");
    client.publish("fechaduraStatus", "Locked");
  }
  else if (fechaduraStatus == "UNLOCKED"){
    //Serial.println("Unlocked");
    client.publish("fechaduraStatus", "Unlocked");
    delay(5000);
    fechaduraStatus = "LOCKED";
  }
}

