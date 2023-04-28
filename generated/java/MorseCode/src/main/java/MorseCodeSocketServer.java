
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.HashMap;
import java.util.Map;

public class MorseCodeSocketServer {
    public static void main(String[] args) {
        Map<Character, String> morseCodeMap = new HashMap<Character, String>() {{
            put('A', ".-");
            put('B', "-...");
            put('C', "-.-.");
            put('D', "-..");
            put('E', ".");
            put('F', "..-.");
            put('G', "--.");
            put('H', "....");
            put('I', "..");
            put('J', ".---");
            put('K', "-.-");
            put('L', ".-..");
            put('M', "--");
            put('N', "-.");
            put('O', "---");
            put('P', ".--.");
            put('Q', "--.-");
            put('R', ".-.");
            put('S', "...");
            put('T', "-");
            put('U', "..-");
            put('V', "...-");
            put('W', ".--");
            put('X', "-..-");
            put('Y', "-.--");
            put('Z', "--..");
        }};

        try (ServerSocket serverSocket = new ServerSocket(8000)) {
            System.out.println("Server started on port 8000");

            while (true) {
                Socket socket = serverSocket.accept();
                System.out.println("New client connected");

                try (PrintWriter out = new PrintWriter(socket.getOutputStream(), true);
                     BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()))) {

                    String inputLine;
                    while ((inputLine = in.readLine()) != null) {
                        // Convert input to uppercase
                        inputLine = inputLine.toUpperCase();

                        // Convert each character to morse code
                        StringBuilder outputLine = new StringBuilder();
                        for (int i = 0; i < inputLine.length(); i++) {
                            char c = inputLine.charAt(i);
                            if (morseCodeMap.containsKey(c)) {
                                outputLine.append(morseCodeMap.get(c) + " ");
                            }
                        }

                        out.println("Morse code: " + outputLine);
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}