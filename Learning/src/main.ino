#include <Arduino.h>

const int pinFive = 5;
int ledValue = LOW;

extern HardwareSerial Serial;

const char maxLength = 5;
char sequence[maxLength];

PROGMEM const char letters[26][maxLength] = {
    ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..",   // A-I
    ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", // J-R
    "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.."         // S-Z
};

void setup()
{
    Serial.begin(9600);
    Serial.println("starting");
    // pinMode(pinFive, INPUT);
    // pinMode(LED_BUILTIN, OUTPUT);
    randomSeed(analogRead(0));
}

void loop()
{
    auto randomIndex = random(0, 26);
    memcpy_P(sequence, letters[randomIndex], maxLength);
    Serial.println(sequence);
    delay(2000);
}