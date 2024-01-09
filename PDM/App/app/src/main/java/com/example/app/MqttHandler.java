package com.example.app;

import org.eclipse.paho.client.mqttv3.*;
import org.eclipse.paho.client.mqttv3.persist.MemoryPersistence;

public class MqttHandler implements MqttCallback {

    private MqttClient client;
    private MqttMessageListener listener;
    private String topic;
    private MqttMessage message;

    public void setListener(MqttMessageListener listener) {
        this.listener = listener;
    }

    public void connect(String brokerUrl, String clientId) {
        try {
            MemoryPersistence persistence = new MemoryPersistence();
            client = new MqttClient(brokerUrl, clientId, persistence);

            // Set this class as the callback for client events
            client.setCallback(this);

            MqttConnectOptions connectOptions = new MqttConnectOptions();
            connectOptions.setCleanSession(true);

            client.connect(connectOptions);
        } catch (MqttException e) {
            e.printStackTrace();
        }
    }

    public void disconnect() {
        try {
            client.disconnect();
        } catch (MqttException e) {
            e.printStackTrace();
        }
    }

    public void publish(String topic, String message) {
        try {
            MqttMessage mqttMessage = new MqttMessage(message.getBytes());
            client.publish(topic, mqttMessage);
        } catch (MqttException e) {
            e.printStackTrace();
        }
    }

    public void subscribe(String topic) {
        try {
            client.subscribe(topic);
        } catch (MqttException e) {
            e.printStackTrace();
        }
    }
    @Override
    public void connectionLost(Throwable cause) {
        System.out.println("Connection lost: " + cause.getMessage());
    }

    @Override
    public void deliveryComplete(IMqttDeliveryToken token) {
        // Called when a message delivery is complete
    }

    public interface MqttMessageListener {
        void onMessageReceived(String topic, String message);
    }

    @Override
    public void messageArrived(String topic, MqttMessage message) throws Exception {
        this.topic = topic;
        this.message = message;
        String receivedMessage = new String(message.getPayload());
        System.out.println("Message received from topic " + topic + ": " + receivedMessage);

        if (listener != null) {
            listener.onMessageReceived(topic, receivedMessage);
        }
    }


}
