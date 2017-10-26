#include <Arduino.h>

const int pinFive = 5;

void setup()
{
    // pinMode(pinFive, INPUT);
    pinMode(pinFive, INPUT_PULLUP);
    Serial.begin(9600);
    Serial.println("starting");
    pinMode(LED_BUILTIN, OUTPUT);
}

void loop()
{
    // if (Serial.available() > 0)
    // {
    //     auto byteData = Serial.read();
    // }
    int reading = digitalRead(pinFive);
    digitalWrite(LED_BUILTIN, !reading);
    Serial.println(reading);
    delay(1000);
}