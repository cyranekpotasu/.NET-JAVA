package snake;

import java.lang.String;

public class Settings {
    private final static Settings settings = new Settings();
    public static Settings getInstance(){return settings;}
    private String difficulty = "hard";
    public  double MS_PER_UPDATE = 1000.0 / 20.0;



    public void changeDiffuculty(){
        if ("easy".equals(difficulty)){
            MS_PER_UPDATE = 1000.0 / 20.0;
        }else
        if ("medium".equals(difficulty)){
            MS_PER_UPDATE = 1000.0 / 30.0;
        }
        else{
            MS_PER_UPDATE = 1000.0 / 40.0;
        };

    }


    public void setDifficulty(String setting) {
        difficulty = setting;
        changeDiffuculty();
    }

}