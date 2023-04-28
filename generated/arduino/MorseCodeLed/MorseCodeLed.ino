
//Define a constant pin for the LED
const int ledPin = 13;

//Set up the serial connection
void setup()
{
  Serial.begin(9600);
  Serial.println("Started!");
  pinMode(ledPin, OUTPUT);
}

//Read input from serial connection and blink the LED in Morse code
void loop()
{
  Serial.println("Ready!");
  //Read the message from the serial connection
  String message = Serial.readString();
  //String morse = "";
  
  //Loop over each character in the message
  for (int i = 0; i < message.length(); i++)
  {
    //Get the Morse code for the current character
    String morseCode = getMorseCode(message.charAt(i));
    //morse += morseCode;

    //Loop over each dot and dash in the Morse code
    for (int j = 0; j < morseCode.length(); j++)
    {
      //Get the current dot or dash
      char dotOrDash = morseCode.charAt(j);
      
      //Turn the LED on
      digitalWrite(ledPin, HIGH);
      
      //Wait for the length of a dot or a dash, respectively
      if (dotOrDash == '.')
        delay(200);
      else
        delay(600);
      
      //Turn the LED off
      digitalWrite(ledPin, LOW);
      
      //Wait in between dots and dashes
      delay(200);
    }
    
    //Wait in between characters
    delay(600);
  }
  
    //Serial.println(message + " -> " + morse);
}

//Get the Morse code for a given character
String getMorseCode(char character)
{
  switch (character)
  {
    case 'A': return ".-";
    case 'B': return "-...";
    case 'C': return "-.-.";
    case 'D': return "-..";
    case 'E': return ".";
    case 'F': return "..-.";
    case 'G': return "--.";
    case 'H': return "....";
    case 'I': return "..";
    case 'J': return ".---";
    case 'K': return "-.-";
    case 'L': return ".-..";
    case 'M': return "--";
    case 'N': return "-.";
    case 'O': return "---";
    case 'P': return ".--.";
    case 'Q': return "--.-";
    case 'R': return ".-.";
    case 'S': return "...";
    case 'T': return "-";
    case 'U': return "..-";
    case 'V': return "...-";
    case 'W': return ".--";
    case 'X': return "-..-";
    case 'Y': return "-.--";
    case 'Z': return "--..";
    case '1': return ".----";
    case '2': return "..---";
    case '3': return "...--";
    case '4': return "....-";
    case '5': return ".....";
    case '6': return "-....";
    case '7': return "--...";
    case '8': return "---..";
    case '9': return "----.";
    case '0': return "-----";
  }
  
  return "";
}