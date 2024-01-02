/*
 ▄▄▄▄▄▄▄▄▄▄▄   ▄▄▄▄▄▄▄▄▄   ▄▄▄▄▄▄▄▄▄▄▄      
▐░░░░░░░░░░░▌ ▐░░░░░░░░░▌ ▐░░░░░░░░░░░▌     
▐░█▀▀▀▀▀▀▀▀▀ ▐░█░█▀▀▀▀▀█░▌▐░█▀▀▀▀▀▀▀█░▌     
▐░▌          ▐░▌▐░▌    ▐░▌▐░▌       ▐░▌    Trabalho Prático de Sistemas Embebidos e de Tempo Real 
▐░▌ ▄▄▄▄▄▄▄▄ ▐░▌ ▐░▌   ▐░▌▐░█▄▄▄▄▄▄▄█░▌    Allan Sales Aleluia, a21990
▐░▌▐░░░░░░░░▌▐░▌  ▐░▌  ▐░▌▐░░░░░░░░░░░▌    Francisco Moreira Rebêlo, a16443 
▐░▌ ▀▀▀▀▀▀█░▌▐░▌   ▐░▌ ▐░▌ ▀▀▀▀▀▀▀▀▀█░▌    José Carlos Paschoal, a 15926   
▐░▌       ▐░▌▐░▌    ▐░▌▐░▌          ▐░▌     
▐░█▄▄▄▄▄▄▄█░▌▐░█▄▄▄▄▄█░█░▌ ▄▄▄▄▄▄▄▄▄█░▌     
▐░░░░░░░░░░░▌ ▐░░░░░░░░░▌ ▐░░░░░░░░░░░▌     
 ▀▀▀▀▀▀▀▀▀▀▀   ▀▀▀▀▀▀▀▀▀   ▀▀▀▀▀▀▀▀▀▀▀      
*/

#include <SPI.h>
#include <Ethernet.h>
#include <MFRC522.h>
#include <MQTT.h>

#include "DHT.h"
#include "pitches.h"

// Definição do endereço MAC e IP do dispositivo
byte mac[] = {0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED};
byte ip[] = {192, 168, 1, 15};

// Definições de pinos e tipos de sensores/dispositivos
#define DHTPIN 2
#define DHTTYPE DHT11
#define SS_PIN 5
#define RST_PIN 9
#define LED_RED 8
#define LED_GREEN 7
#define LED_BLUE 6
#define BUZZER 4

// Inicialização dos objetos e variáveis globais
EthernetClient net;
MQTTClient client;
DHT dht(DHTPIN, DHTTYPE);
MFRC522 rfid(SS_PIN, RST_PIN);
MFRC522::MIFARE_Key key;

byte nuidPICC[4];
unsigned long lastDhtReadTime = 0;
const unsigned long intervalDHT = 60000;
float tempSetpoint = 21;
float tempActual;
String fechaduraStatus = "LOCKED";

void setup() {
    Serial.begin(9600); // Inicia a comunicação serial
    Ethernet.begin(mac, ip); // Inicia a conexão Ethernet com o endereço MAC e IP especificados

    client.begin("192.168.1.14", net); // Inicia o cliente MQTT com o endereço do servidor MQTT
    client.onMessage(messageReceived);  // Define a função de callback para mensagens MQTT

    connect();// Estabelece a conexão com o servidor MQTT

    dht.begin(); // Inicializa o sensor DHT
    SPI.begin(); // Inicializa a comunicação SPI
    rfid.PCD_Init(); // Inicializa o módulo RFID

    // Define a chave para o módulo RFID
    for (byte i = 0; i < 6; i++) {
        key.keyByte[i] = 0xFF;
    }
}

void loop() {
    client.loop(); // Mantém a conexão MQTT ativa

    // Reconecta ao servidor MQTT, se necessário
    if (!client.connected()) {
        connect();
    }

    rfidRead();// Função para ler o cartão RFID

    ac(tempSetpoint, tempActual); // Função para controlar o sistema de ar condicionado
    fechadura(fechaduraStatus); // Função para controlar o estado da fechadura
    readDht();// Lê a temperatura atual e publica no MQTT

    if (millis() - lastDhtReadTime >= intervalDHT) {
        readDht(); // Lê a temperatura atual e publica no MQTT
        lastDhtReadTime = millis();
    }
}

// Lê a temperatura atual do sensor DHT e publica no MQTT
void readDht() {
    tempActual = dht.readTemperature();

    String temperatureString = String(tempActual);
    client.publish("actualTemp", temperatureString.c_str());
}

// Lê o cartão RFID e realiza ações correspondentes
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

// Imprime os bytes em formato hexadecimal
void printHex(byte *buffer, byte bufferSize) {
    for (byte i = 0; i < bufferSize; i++) {
        Serial.print(buffer[i] < 0x10 ? " 0" : " ");
        Serial.print(buffer[i], HEX);
    }
}

// Estabelece a conexão com o servidor MQTT
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

// Função de callback para mensagens MQTT recebidas
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

// Controla o sistema de ar condicionado com base na temperatura atual e setpoint
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
    //String tempSetpointStr = String(tempSetpoint);  // Convert float to String
    //client.publish("tempSetpoint", tempSetpointStr.c_str());  // Publish to MQTT
}

// Controla o estado da fechadura
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

