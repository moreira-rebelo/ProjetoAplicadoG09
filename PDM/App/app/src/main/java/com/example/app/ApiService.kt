// ApiService.kt
package com.example.app

import ReservationResponse
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.Header
import retrofit2.http.POST

interface ApiService {

    @POST("reservation/login") // Endpoint URL for the login API
    fun loginUser(@Body loginRequest: LoginRequest): Call<LoginResponse>

    @GET("reservation")
    fun getReservation(@Header("Authorization") token: String): Call<ReservationResponse>
}
