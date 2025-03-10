from socket import *

serverName = "localhost"
serverPort = 42069

# Opret TCP-klient
clientSocket = socket(AF_INET, SOCK_STREAM)
clientSocket.connect((serverName, serverPort))
print(f"Forbundet til serveren på port {serverPort}")


while True:
    # 1. Indtast kommando
    command = input("Vælg kommando (Random, Add, Subtract). Tast 'q' for at afslutte: ")
    if command.lower() == 'q':
        print("Afslutter klienten.")
        break

    # Send kommando
    clientSocket.sendall((command + "\n").encode())

    # 2. Vent på svar ("Input numbers")
    response = clientSocket.recv(1024).decode().strip()
    if not response:
        print("Ingen svar fra server. Afbryder.")
        break
    print("Server:", response)

    # 3. Indtast 2 tal
    tal1 = input("Første tal: ")
    tal2 = input("Andet tal: ")
    clientSocket.sendall((tal1 + " " + tal2 + "\n").encode())

    # 4. Læs resultatet fra serveren
    result = clientSocket.recv(1024).decode().strip()
    if not result:
        print("Ingen resultat modtaget. Afbryder.")
        break
    print("Result:", result)

# Luk forbindelsen til sidst
clientSocket.close()
