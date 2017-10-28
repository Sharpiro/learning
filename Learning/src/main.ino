#include <Arduino.h>

const int pinFive = 5;
int ledValue = LOW;
extern HardwareSerial Serial;

enum State
{
    Main,
    PokeTypes
};

enum PokeTypesState
{
    Move,
    Type
};

State gameState;
PokeTypesState pokeTypesState;
int move;
int type;

const char maxLength = 5;

void setup()
{
    Serial.begin(9600);
    Serial.setTimeout(30);
    Serial.println("starting");
    // pinMode(pinFive, INPUT);
    // pinMode(LED_BUILTIN, OUTPUT);
    onMainState();
}

void loop()
{
    if (Serial.available())
    {
        auto start = millis();
        // while (Serial.available())
        // {
        //     char rawData = Serial.read();
        //     Serial.print(rawData);
        //     // char data[1] = {rawData};
        // }
        // Serial.println();
        // String rawData = Serial.readString();
        byte buffer[2];
        Serial.readBytes(buffer, 2);
        // auto data = rawData.c_str();
        auto data = (char *)buffer;
        int input = strtol(data, nullptr, 10);
        Serial.println(input);
        // switch (gameState)
        // {
        // case State::Main:
        //     handleMainStateInput(input);
        //     break;
        // case State::PokeTypes:
        //     handlePokeTypesState(input);
        //     break;
        // }
        Serial.print("duration: ");
        Serial.println(millis() - start);
    }
}

void onMainState()
{
    gameState = State::Main;
    Serial.println("Entered main state");
    Serial.println("Press 1 for PokeTypes program");
}

void handleMainStateInput(int input)
{
    switch (input)
    {
    case State::PokeTypes:
        OnPokeState();
        break;
    }
}

void OnPokeState()
{
    gameState = State::PokeTypes;
    Serial.println("Entered PokeTypes state");
    onMovesState();
}

void handlePokeTypesState(int input)
{
    if (input < 0)
    {
        pokeTypesState = PokeTypesState::Move;
        onMainState();
        return;
    }
    switch (pokeTypesState)
    {
    case PokeTypesState::Move:
        move = input;
        onPokeTypeState();
        break;
    case PokeTypesState::Type:
        type = input;
        pokeTypesComplete();
        break;
    }
}

void onMovesState()
{
    pokeTypesState = PokeTypesState::Move;
    Serial.println("Enter move type");
}

void onPokeTypeState()
{
    pokeTypesState = PokeTypesState::Type;
    Serial.println("Enter poke type");
}

void pokeTypesComplete()
{
    Serial.print("move: ");
    Serial.println(move);
    Serial.print("type: ");
    Serial.println(type);
    Serial.println("severity is 0.5 (not true)");
    onMovesState();
}