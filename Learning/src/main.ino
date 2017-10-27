#include <Bounce2.h>
#include <Arduino.h>

const int pinFive = 5;
int ledValue = LOW;
Bounce bouncer = Bounce();

extern HardwareSerial Serial;

void setup()
{
    Serial.begin(9600);
    Serial.println("starting");
    // pinMode(pinFive, INPUT);
    pinMode(pinFive, INPUT_PULLUP);
    pinMode(LED_BUILTIN, OUTPUT);
    bouncer.attach(pinFive);
}

void loop()
{
    if (bouncer.update()) 
    {
        if (bouncer.read() == LOW)
        {
            ledValue = !ledValue;
            digitalWrite(LED_BUILTIN, ledValue);
        }
    }
}