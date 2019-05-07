import java.awt.Color;
import java.awt.Dimension;
import java.awt.Toolkit;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.Random;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;


public class  ButtonClick extends javax.swing.JFrame
{ 
    private Random d = new Random();
    Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
    public ButtonClick() 
    {
        	setSize(screenSize); //Ustawienie rozmiaru okna 300x300
            setLocationRelativeTo(null); //Ustawienie polozenie okna
            setUndecorated(true);
            JPanel panel = new JPanel(); 
        panel.add(createButton()); //Dodanie przycisku do kontenera - panelu
        setContentPane(panel);
        setBackground(new Color (0f,0f,0f,0f));
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    private JButton createButton() 
    {

    
		final JButton button = new JButton("Button");//Tworzenie przycisku
        button.addMouseListener(new MouseAdapter() //Dodanie MauseListener do przycisku akcji
        		
        {
            @Override
            public void mouseClicked(MouseEvent e) 
            {
                int a = d.nextInt(screenSize.width-100); //Funkcja losuje zmienna miedzy 0 a 200
                int b = d.nextInt(screenSize.height-50);
                button.setLocation(a,b); //Przenoszenie komponentu do nowej lokalizacji
            }
        });
        return button;
    }

    public static void main(String[] args) 
    {
                new ButtonClick().setVisible(true);
    }
}
