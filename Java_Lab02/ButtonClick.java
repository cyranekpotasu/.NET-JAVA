import java.awt.Color;
import java.awt.Dimension;
import java.awt.Frame;
import java.awt.Toolkit;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.Random;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.SwingUtilities;
import java.util.TimerTask;
import java.util.Timer;

public class ButtonClick extends javax.swing.JFrame
{ 
	Timer timer=new Timer();
    private Random d = new Random();
    public JButton przycisk = new JButton();
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

    	
    
        final JButton button = new JButton("Button");//Tworzenie przycisku
        button.addMouseListener(new MouseAdapter() //Dodanie MauseListener do przycisku akcji
        		// interfejs s�uchacza zdarze� myszy powiedzialny za klikni�cia i pojawianie si� 
        		//kursora nad komponentem nas�uchuj�cym
        {
            @Override
            public void mouseClicked(MouseEvent e) //Wywo�ywany, gdy mysz wchodzi do komponentu
            {
                int a = d.nextInt(screenSize.width-100); //Funkcja losuje zmienna miedzy 0 a 200
                int b = d.nextInt(screenSize.height-50);
                int startPositionX = button.getX();
                int startPositionY = button.getY();
                button.setLocation(a,b); //Przenoszenie komponentu do nowej lokalizacji
            	int difx = a-startPositionX;
            	int dify = b-startPositionY;
            	
                timer.schedule(new TimerTask() {
        			@Override
        			public void run() {
        				int x,y;
        				int x1=startPositionX;
	                	int y1=startPositionY;
        				if(difx/Math.abs(difx)<1)
        					x=1;
        				else
	                	x=difx/Math.abs(difx);
        				if(dify/Math.abs(difx)<1)
        					y=1;
        				else
        				y=dify/Math.abs(difx);
        		         int j=startPositionY;
        	                for(int i =0;i<Math.abs(difx);i++)
        	                {
        	                	x1=x+x1;
        	                	y1=y1+y;
        	                	/*System.out.printf("%i",x1);*/
        	                	button.setLocation(x1, y1);
        	                
        	               /* {
        	                
        							 if(j<b)
        							 {
        	                		 button.setLocation(i, j); //Przenoszenie komponentu do nowej lokalizacji
        		                	 j++;
        							 } */
        							 try
        		            {
        		                Thread.sleep(5);
        		            } catch (InterruptedException e)
        		            {
        		                e.printStackTrace();
        		            }
        	                }
        		        }
        		    }, 100);
            }
        });
        return button;
    }

    public static void main(String[] args) 
    {
      new ButtonClick().setVisible(true);
    }
}
