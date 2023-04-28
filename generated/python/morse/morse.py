import socket
import sys

# Morse code dictionary
MORSE_CODE_DICT = { 'A':'.-', 'B':'-...', 
                    'C':'-.-.', 'D':'-..', 'E':'.', 
                    'F':'..-.', 'G':'--.', 'H':'....', 
                    'I':'..', 'J':'.---', 'K':'-.-', 
                    'L':'.-..', 'M':'--', 'N':'-.', 
                    'O':'---', 'P':'.--.', 'Q':'--.-', 
                    'R':'.-.', 'S':'...', 'T':'-', 
                    'U':'..-', 'V':'...-', 'W':'.--', 
                    'X':'-..-', 'Y':'-.--', 'Z':'--..', 
                    '1':'.----', '2':'..---', '3':'...--', 
                    '4':'....-', '5':'.....', '6':'-....', 
                    '7':'--...', '8':'---..', '9':'----.', 
                    '0':'-----', ', ':'--..--', '.':'.-.-.-', 
                    '?':'..--..', '/':'-..-.', '-':'-....-', 
                    '(':'-.--.', ')':'-.--.-'} 

# Create a TCP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Bind the socket to the port
server_address = ('localhost', 10000)
print("starting up on %s port %s" % server_address)
sock.bind(server_address)

# Listen for incoming connections
sock.listen(1)

while True:
    # Wait for a connection
    print("waiting for a connection")
    connection, client_address = sock.accept()
    try:
        print("connection from", client_address)

        # Receive the data in small chunks and retransmit it
        while True:
            data = connection.recv(16)
            print("received %s" % data)
            if data:
                # Convert text to morse code
                output = ''
                for letter in data:
                    output += MORSE_CODE_DICT.get(chr(letter).upper(), ' ') + ' '
                # Send back the result
                connection.sendall(output.encode())
            else:
                print("no more data from", client_address)
                break
            
    finally:
        # Clean up the connection
        connection.close()