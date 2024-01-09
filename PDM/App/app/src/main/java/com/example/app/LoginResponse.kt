// LoginResponse.kt
package com.example.app

data class LoginResponse(
    val data: TokenData?
)

data class TokenData(
    val token: String?
)
