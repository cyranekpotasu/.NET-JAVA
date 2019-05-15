import java.awt.Color;
import java.awt.Dimension;
import java.awt.Toolkit;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.Random;
import javax.swing.Timer;
import java.util.TimerTask;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;

import java.awt.EventQueue;

import javafx.event.ActionEvent;
import java.awt.event.ActionListener;


public class ButtonClick extends javax.swing.JFrame
{ 
    private static final long serialVersionUID = 1L;
    int x, y, xznak, yznak, a, b;
    final JButton button = new JButton("Button");//Tworzenie przycisku
	Timer timer=new Timer(20, null);
    private Random d = new Random();
    Thread thread;
    Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
    public ButtonClick() 
    {
        	setSize(screenSize); //Ustawienie rozmiaru okna 300x300
            setLocationRelativeTo(null); //Ustawienie polozenie okna
            setUndecorated(true);
            JPanel panel = new JPanel(); //Tworzenie panelu za pomoc� konstruktora bez tytulu
        panel.add(createButton()); //Dodanie przycisku do kontenera - panelu
        setContentPane(panel);
        setBackground(new Color (0f,0f,0f,0f));
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE); //Opcja przycisku zamkni�cia - wyjscie z aplikacji
    }

    private JButton createButton() 
    {

    	
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                timer.addActionListener(new ActionListener(){
                    @Override
                    public void actionPerformed(java.awt.event.ActionEvent e) {
                        if(x == a || y == b)
                        {
                            timer.stop();
                        }
                        else
                            changeState(button);
                        }
                    });
                    
        button.addActionListener(new ActionListener()
        {
                    @Override
                    public void actionPerformed(java.awt.event.ActionEvent e) {
                        a = d.nextInt(screenSize.width-100); //Funkcja losuje zmienna miedzy 0 a 200
                        b = d.nextInt(screenSize.height-50);
                        x = button.getX();
                        y = button.getY();
                        timer.start();
                    }
        });
    }
    });
    return button;
    }

void changeState( JButton button)
{
    xznak =1;
    yznak =1;
    if(x>a)
        xznak =-1;
    if(y>b)
        yznak =-1;
    if(x!=a)
        x=x+xznak;
    if(x!=b)
        y=y+yznak;

   button.setLocation(x, y);
}

    public static void main(String[] args) 
    {
      new ButtonClick().setVisible(true);
    }
}
