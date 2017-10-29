#include <Arduino.h>
#include "MemoryFree.h"

const int pinFive = 5;
int ledValue = LOW;
extern HardwareSerial Serial;

void setup()
{
    Serial.begin(9600);
    Serial.println("starting");
    pinMode(pinFive, OUTPUT);
    pinMode(LED_BUILTIN, OUTPUT);
    digitalWrite(LED_BUILTIN, LOW);

    Serial.print("free memory: ");
    Serial.println(freeMemory());
}

void loop()
{
    if (Serial.available())
    {
        char input = Serial.read();

        if (input == '1')
        {
            digitalWrite(pinFive, HIGH);
            Serial.println("turning power on...");
        }
        else if (input == '0')
        {
            digitalWrite(pinFive, LOW);
            Serial.println("turning power off...");
        }
    }
}