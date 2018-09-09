package com.BecomeHuman;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.*;
import java.net.InetAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.UnknownHostException;

public class Server extends Thread {

    private static final int port = 8607;
    protected static String server_IP;

    public static void main(String[] args)  {


        try{
            InetAddress iAddress = InetAddress.getLocalHost();
            server_IP = iAddress.getHostAddress();
            System.out.println("Server IP : " + server_IP);

        }catch (UnknownHostException e){

        }

        try {


            //Server
            ServerSocket serverSocket = new ServerSocket(port);
            System.out.println("Server has started");

            //Accepting client
            System.out.println("Waiting");
            Socket socket = serverSocket.accept();
            System.out.println("Client has been added");

            //Send message to client
            BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));
            bw.write("Connection accepted");
            bw.newLine();
            bw.flush();

            //receive json from client
            BufferedReader br = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            JSONObject json = new JSONObject(br.readLine());
            System.out.println(json.toString());

            //send json accepted
            bw.write("JSONObject has sent");
            bw.newLine();
            bw.flush();


        } catch (IOException | JSONException e) {
            e.printStackTrace();
        }

    }
}
