#include <Arduino.h>

const int interruptPin = 2;
const int pinFive = 5;
int ledValue = LOW;

extern HardwareSerial Serial;

void beEratic()
{
    Serial.println("changed everything");
}

void setup()
{
    Serial.begin(9600);
    Serial.println("starting");
    // pinMode(pinFive, INPUT);
    pinMode(interruptPin, INPUT_PULLUP);
    pinMode(LED_BUILTIN, OUTPUT);
    auto analogReading = analogRead(0);
    randomSeed(analogReading);
    attachInterrupt(0, beEratic, CHANGE);
}

void loop()
{
    auto number = random(1, 10);
    Serial.println(number);
    delay(1000);
}