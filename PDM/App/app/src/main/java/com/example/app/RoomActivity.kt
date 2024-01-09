package com.example.app

import ReservationResponse
import android.os.Bundle
import android.widget.FrameLayout
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class RoomActivity : AppCompatActivity(), MqttHandler.MqttMessageListener {

    private val BROKER_URL = "tcp://192.168.1.14:1883"
    private val CLIENT_ID = "2"

    private lateinit var mqttHandler: MqttHandler
    private lateinit var textViewActual: TextView
    private lateinit var textViewModoAc: TextView
    private lateinit var textViewFechaduraStatus: TextView
    private lateinit var textViewSetpoint: TextView
    private lateinit var textViewRoomNumber: TextView
    private lateinit var token: String

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.room_main)

        textViewActual = findViewById(R.id.textViewActual)
        textViewModoAc = findViewById(R.id.textViewModoAc)
        textViewFechaduraStatus = findViewById(R.id.textViewEstadoFechadura)
        textViewSetpoint = findViewById(R.id.textViewSetpoint)
        textViewRoomNumber = findViewById(R.id.textViewRoomNumber)

        mqttHandler = MqttHandler()
        mqttHandler.setListener(this)
        mqttHandler.connect(BROKER_URL, CLIENT_ID)

        subscribeToTopic("actualTemp")
        subscribeToTopic("acStatus")
        subscribeToTopic("fechaduraStatus")
        subscribeToTopic("tempSetpoint")

        publishMessage("tempSetpoint", "21.0")

        token = intent.getStringExtra("TOKEN_KEY") ?: ""

        if (token.isNotEmpty()) {
            fetchReservationDetails()
        } else {
            Toast.makeText(this, "Token not found", Toast.LENGTH_SHORT).show()
            finish() // Close the activity if token is not available
        }

        val botaoClimate: FrameLayout = findViewById(R.id.frameClimate)
        botaoClimate.setOnClickListener{
            val roomClimateFragment = RoomClimate()
            val fragmentManager = supportFragmentManager
            roomClimateFragment.arguments = Bundle().apply {
                putString("actualTemp", textViewActual.text.toString())
                putString("acStatus", textViewModoAc.text.toString())
                putString("tempSetpoint", textViewSetpoint.text.toString())
            }
            roomClimateFragment.show(fragmentManager, "RoomClimateFragment")
        }


        val botaoDoor: FrameLayout = findViewById(R.id.frameDoor)
        botaoDoor.setOnClickListener{
            publishMessage("fechaduraActuator", "ON")
        }

    }

    override fun onDestroy() {
        mqttHandler.disconnect()
        super.onDestroy()
    }

    private fun subscribeToTopic(topic: String) {
        mqttHandler.subscribe(topic)
    }

    override fun onMessageReceived(topic: String, message: String) {
        when (topic) {
            "actualTemp" -> runOnUiThread { textViewActual.text = "$message ºC" }
            "acStatus" -> runOnUiThread { textViewModoAc.text = message }
            "fechaduraStatus" -> runOnUiThread { textViewFechaduraStatus.text = message }
            "tempSetpoint" -> runOnUiThread { textViewSetpoint.text = "$message ºC" }
        }
    }

    private fun publishMessage(topic: String, message: String) {
        Toast.makeText(this, "Publishing MQTT message: $message", Toast.LENGTH_SHORT).show()
        mqttHandler.subscribe(topic)
        mqttHandler.publish(topic, message)
    }

    private fun fetchReservationDetails() {
        val retrofit = Retrofit.Builder()
            .baseUrl("http://13.53.36.123/")
            .addConverterFactory(GsonConverterFactory.create())
            .build()

        val apiService = retrofit.create(ApiService::class.java)

        apiService.getReservation("Bearer $token").enqueue(object : Callback<ReservationResponse> {
            override fun onResponse(call: Call<ReservationResponse>, response: Response<ReservationResponse>) {
                if (response.isSuccessful) {
                    val reservationResponse = response.body()
                    if (reservationResponse != null) {
                        textViewRoomNumber.text = reservationResponse.data.roomNumber
                    }
                } else {
                    Toast.makeText(this@RoomActivity, "Failed to fetch reservation details", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<ReservationResponse>, t: Throwable) {
                Toast.makeText(this@RoomActivity, "Error: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }


}