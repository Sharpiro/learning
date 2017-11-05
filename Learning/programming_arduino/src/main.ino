#include <Arduino.h>
#include <LiquidCrystal.h>

// const int pinFive = 5;
extern HardwareSerial Serial;
LiquidCrystal lcd(8, 9, 4, 5, 6, 7);
int numRows = 2;
int numColumns = 16;
// int count = 0;
const char message[] = "sup witches";
int messageLength = 11;
int index = messageLength - 1;

void setup()
{
    Serial.begin(9600);
    Serial.println("starting");
    lcd.begin(numRows, numColumns);
    lcd.clear();
    // lcd.setCursor(0, 0);
    // lcd.print("sup");
    // lcd.setCursor(0, 1);
    // lcd.print("witches");
    // pinMode(LED_BUILTIN, OUTPUT);
    // digitalWrite(LED_BUILTIN, LOW);
}

void loop()
{
    if (index < 0)
    {
        lcd.clear();
        index = messageLength - 1;
        delay(1000);
    }
    lcd.setCursor(index, 0);
    lcd.print(message[index]);
    index--;
    //time
    // lcd.setCursor(0, 1);
    // lcd.print(++count);
    if (message[index] != ' ')
        delay(500);
}