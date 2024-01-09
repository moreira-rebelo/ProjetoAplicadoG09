package com.example.app

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import android.widget.Toast
import com.google.android.material.bottomsheet.BottomSheetDialogFragment

class RoomClimate : BottomSheetDialogFragment(), MqttHandler.MqttMessageListener  {

    private val BROKER_URL = "tcp://192.168.1.14:1883"
    private val CLIENT_ID = "3"

    private lateinit var mqttHandler: MqttHandler
    private lateinit var textViewActual2: TextView
    private lateinit var textViewModoAc2: TextView
    private lateinit var textViewSetpoint2: TextView

    private var tempSetpointValueNew: Float = 0.0f

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return inflater.inflate(R.layout.fragment_room_climate, container, false)
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        mqttHandler = MqttHandler()
        mqttHandler.setListener(this)
        mqttHandler.connect(BROKER_URL, CLIENT_ID)


        // Initialize TextViews
        textViewActual2 = view.findViewById(R.id.textViewActual2)
        textViewModoAc2 = view.findViewById(R.id.textViewModoAc2)
        textViewSetpoint2 = view.findViewById(R.id.textViewSetpoint2)

        // Retrieve arguments from RoomActivity
        val actualTempValue = arguments?.getString("actualTemp", "") ?: ""
        val acStatusValue = arguments?.getString("acStatus", "") ?: ""
        val tempSetpointValue = arguments?.getString("tempSetpoint", "") ?: ""


        val numberPattern = """-?\d+(\.\d+)?""".toRegex()
        val matchResult = numberPattern.find(tempSetpointValue)
        val numberAsString = matchResult?.value
        var tempSetpointValueNew: Float = numberAsString?.toFloatOrNull() ?: 0.0f



        textViewActual2.text = actualTempValue
        textViewModoAc2.text = acStatusValue
        textViewSetpoint2.text = tempSetpointValue

        // Button Plus and Minus Logic
        val buttonPlus: ImageView = view.findViewById(R.id.imageViewPlus) // Assuming you named it imageViewPlus in your XML
        val buttonMinus: ImageView = view.findViewById(R.id.imageViewMinus) // Assuming you named it imageViewMinus in your XML


        buttonPlus.setOnClickListener {
            Log.d("RoomClimate", "Before incrementing, tempSetpointValueNew: $tempSetpointValueNew")
            incrementSetpointAndPublish(tempSetpointValueNew)
        }

        buttonMinus.setOnClickListener {
            Log.d("RoomClimate", "Before decrementing, tempSetpointValueNew: $tempSetpointValueNew")
            decrementSetpointAndPublish(tempSetpointValueNew)
        }

    }

    private fun incrementSetpointAndPublish(tempSetpointValueNew: Float) {
        Log.d("RoomClimate", "Inside incrementSetpointAndPublish, tempSetpointValueNew before increment: $tempSetpointValueNew")

        var currentSetpoint = tempSetpointValueNew
        currentSetpoint += 0.5f

        Log.d("RoomClimate", "Inside incrementSetpointAndPublish, currentSetpoint after increment: $currentSetpoint")

        if (currentSetpoint > 32f) {
            currentSetpoint = 32f
        }

        updateSetpointAndPublish(currentSetpoint)
    }

    private fun decrementSetpointAndPublish(tempSetpointValueNew: Float) {
        var currentSetpoint = tempSetpointValueNew

        currentSetpoint -= 0.5f
        if (currentSetpoint < 16f) {
            currentSetpoint = 16f
        }
        updateSetpointAndPublish(currentSetpoint)
    }

    private fun updateSetpointAndPublish(newSetpoint: Float) {
        textViewSetpoint2.text = String.format("%.1f", newSetpoint)
        mqttHandler.publish("tempSetpoint", newSetpoint.toString())

    }

    override fun onDestroy() {
        mqttHandler.disconnect()
        super.onDestroy()
    }

    private fun subscribeToTopic(topic: String) {
        mqttHandler.subscribe(topic)
    }

    private fun publishMessage(topic: String, message: String) {
        mqttHandler.subscribe(topic)
        mqttHandler.publish(topic, message)
    }

    override fun onMessageReceived(topic: String?, message: String?) {
    }

}
