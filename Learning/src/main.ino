#include <Arduino.h>
#include <eeprom.h>

const int pinFive = 5;
int ledValue = LOW;

extern HardwareSerial Serial;

const char maxLength = 5;
char sequence[maxLength];

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
    delay(2000);
}

void hardEEProm()
{
    auto charr = EEPROM.read(addr);
    EEPROM.write(0, 'A');
    float x = 12.2;
    EEPROM.write(0, highByte(x));
    EEPROM.write(1, lowByte(x));

    byte highB = EEPROM.read(0);
    byte lowB = EEPROM.read(1);

    float newX = (highB << 8) + lowB;
}

// void easyEEProm()
// {
//     float x = 12.2;
//     eeprom_write_block(x, 0, 4);

//     float y;
//     eeprom_read_block(y, 0, 4);
//     Serial.println(y);
// }
