#include <Arduino.h>

const int pinFive = 5;
int readingValue = LOW;

extern HardwareSerial Serial;

byte receiveBytes(byte *data)
{
    auto availableBytes = Serial.available();
    if (!availableBytes)
        return 0;
    data = (byte *)malloc(sizeof(byte) * availableBytes);
    for (auto i = 0; i < availableBytes; i++)
    {
        auto reading = (byte)Serial.read();
        Serial.print(reading);
        data[i] = reading;
    }
    return availableBytes;
}

void setup()
{
    Serial.begin(9600);
    Serial.println("starting");
    // pinMode(pinFive, INPUT);
    pinMode(pinFive, INPUT_PULLUP);
    pinMode(LED_BUILTIN, OUTPUT);
}

void loop()
{
    byte *data;
    auto dataLength = receiveBytes(data);
    for (auto i = 0; i < dataLength; i++)
    {
        Serial.print(data[i]);
    }
    if (dataLength)
        Serial.println();
    delete (data);
    auto reading = !digitalRead(pinFive);
    if (reading == HIGH && readingValue != HIGH)
    {
        readingValue = HIGH;
        Serial.println(reading);
        digitalWrite(LED_BUILTIN, readingValue);
        delay(500);
    }
    else if (reading == LOW && readingValue != LOW)
    {
        readingValue = LOW;
        Serial.println(reading);
        digitalWrite(LED_BUILTIN, readingValue);
        delay(500);
    }
}