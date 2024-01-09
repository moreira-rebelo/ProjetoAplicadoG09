// Define the main data class for the entire response
data class ReservationResponse(
    val data: ReservationData
)

// Define a nested data class to represent the "data" field inside the main response
data class ReservationData(
    val reservationCode: String,
    val roomNumber: String,
    val checkIn: String,
    val checkOut: String,
    val isValid: Boolean
)