#include <Arduino.h>
#include <LiquidCrystal.h>
#include "LcdKeys.h"

extern HardwareSerial Serial;
LiquidCrystal lcd(8, 9, 4, 5, 6, 7);

const int numRows = 2;
const int numColumns = 16;

int lastTime;
int lastButton;
int lcd_key = 0;
int adc_key_in = 0;

int selectIndex;
const char data[3][4] = {"NOR", "FIR", "WAT"};

void setup()
{
    Serial.begin(9600);
    Serial.println("starting");
    lcd.begin(numRows, numColumns);
    lcd.clear();
    lcd.setCursor(0, 0);
    lcd.print("Push the buttons");
}

void loop()
{
    int upTime = millis() / 1000;
    if (upTime > lastTime)
    {
        lcd.setCursor(9, 1);
        lastTime = upTime;
        lcd.print(upTime);
        // Serial.print("new time: ");
        // Serial.println(upTime);
    }

    adc_key_in = analogRead(0);
    lcd_key = lcdKeys::read_LCD_buttons(adc_key_in);

    if (lcd_key == lastButton)
        return;

    lcd.setCursor(0, 1);
    Serial.print("new input: ");
    Serial.println(adc_key_in);
    Serial.println(lcd_key);
    switch (lcd_key)
    {
    case btnRight:
    {
        lcd.print("RIGHT ");
        break;
    }
    case btnLeft:
    {
        lcd.print("LEFT   ");
        break;
    }
    case btnUp:
    {
        lcd.print("UP    ");
        break;
    }
    case btnDown:
    {
        lcd.print("DOWN  ");
        break;
    }
    case btnSelect:
    {
        lcd.print("SELECT");
        break;
    }
    case btnNone:
    {
        lcd.print("NONE  ");
        break;
    }
    }
    lastButton = lcd_key;
}