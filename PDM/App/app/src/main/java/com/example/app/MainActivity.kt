package com.example.app

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class MainActivity : AppCompatActivity(), MqttHandler.MqttMessageListener {

    private val BROKER_URL = "tcp://192.168.1.14:1883"
    private val CLIENT_ID = "1"

    private lateinit var mqttHandler: MqttHandler

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        mqttHandler = MqttHandler()
        mqttHandler.setListener(this)
        mqttHandler.connect(BROKER_URL, CLIENT_ID)


        val botaoLogin: Button = findViewById(R.id.buttonLogin)
        botaoLogin.setOnClickListener {
            val reservationCode = findViewById<EditText>(R.id.editTextText2).text.toString()
            val reservationPassword = findViewById<EditText>(R.id.editTextPassword).text.toString()
            loginUser(reservationCode, reservationPassword)
            //navigateToRoomMain("test")
        }
    }

    private fun loginUser(reservationCode: String, reservationPassword: String) {
        val retrofit = Retrofit.Builder()
            .baseUrl("http://13.53.36.123/")
            .addConverterFactory(GsonConverterFactory.create())
            .build()

        val apiService = retrofit.create(ApiService::class.java)
        val loginRequest = LoginRequest(reservationCode, reservationPassword)

        apiService.loginUser(loginRequest).enqueue(object : Callback<LoginResponse> {
            override fun onResponse(call: Call<LoginResponse>, response: Response<LoginResponse>) {
                if (response.isSuccessful) {
                    val loginResponse = response.body()

                    if (loginResponse != null && loginResponse.data?.token != null) {
                        val token = loginResponse.data.token
                        navigateToRoomMain(token)
                    } else {
                        Toast.makeText(this@MainActivity, "Login failed: Invalid response body", Toast.LENGTH_SHORT).show()
                    }
                } else {
                    val errorResponse = response.errorBody()?.string()
                    Toast.makeText(this@MainActivity, "Login failed: $errorResponse", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<LoginResponse>, t: Throwable) {
                Log.e("LoginFailure", "Login failed: ${t.message}")
                Toast.makeText(this@MainActivity, "Login failed: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }

    private fun navigateToRoomMain(token: String) {
        val intent = Intent(this@MainActivity, RoomActivity::class.java)
        intent.putExtra("TOKEN_KEY", token)
        startActivity(intent)
        Toast.makeText(this@MainActivity, "Login successful", Toast.LENGTH_SHORT).show()
    }

    override fun onDestroy() {
        mqttHandler.disconnect()
        super.onDestroy()
    }

    private fun publishMessage(topic: String, message: String) {
        //Toast.makeText(this, "Publishing MQTT message: $message", Toast.LENGTH_SHORT).show()
        mqttHandler.subscribe(topic)
        mqttHandler.publish(topic, message)
    }

    override fun onMessageReceived(topic: String, message: String) {
        // Handle the received message if needed
    }
}
