#include "LcdKeys.h"

namespace lcdKeys
{
int read_LCD_buttons(int analogValue)
{
    // buttons when read are centered at these valies: 0, 144, 329, 504, 741
    if (analogValue > 1000)
        return btnNone;

    // // For V1.1 us this threshold
    // if (analogValue < 75)
    //     return btnRight;
    // if (analogValue < 250)
    //     return btnUp;
    // if (analogValue < 450)
    //     return btnDown;
    // if (analogValue < 650)
    //     return btnLeft;
    // if (analogValue < 850)
    //     return btnSelect;

    // For V1.0 comment the other threshold and use the one below:
    if (analogValue < 85)
        return btnRight;
    if (analogValue < 195)
        return btnUp;
    if (analogValue < 380)
        return btnDown;
    if (analogValue < 555)
        return btnLeft;
    if (analogValue < 790)
        return btnSelect;

    return btnNone;
}
}