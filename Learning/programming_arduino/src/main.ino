#include <Arduino.h>
#include "MemoryFree.h"
#include "Chatpad.h"

// const int pinFive = 5;
extern HardwareSerial Serial;
int ledState = 0;
Chatpad pad;

void print_keys(Chatpad &pad, Chatpad::keycode_t code, Chatpad::eventtype_t type)
{
    if (type == Chatpad::Down)
    {
        char a = pad.toAscii(code);
        if (a != 0)
        {
            ledState = !ledState;
            digitalWrite(LED_BUILTIN, HIGH);
            // Serial.print(a);
        }
    }
}

void setup()
{
    // Serial.begin(57600);
    // Serial.println("starting");
    pad.init(Serial, print_keys);
    pinMode(LED_BUILTIN, OUTPUT);
    digitalWrite(LED_BUILTIN, LOW);
}

void loop()
{
    pad.poll();
}