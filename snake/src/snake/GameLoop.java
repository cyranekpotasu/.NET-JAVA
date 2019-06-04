package snake;

import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.Color;

public class GameLoop extends Thread {
    private final GameController controller;
    private final GraphicsContext context;
    private final Grid grid;
    private boolean running = false;
    Settings settings = Settings.getInstance();

    GameLoop(final GameController controller, final int snakeSpeed) {
        this.controller = controller;

        Canvas canvas = this.controller.getCanvas();
        Snake snake = new Snake(new Vec2D((int) (canvas.getWidth() / 2),
                (int) (canvas.getHeight() / 2)), snakeSpeed);
        context = canvas.getGraphicsContext2D();
        grid = new Grid(canvas.getWidth(), canvas.getHeight(), snake);
        canvas.setOnKeyPressed(new KeyHandler(grid));
    }

    @Override
    public void run() {
        synchronized (this) {
            running = true;

            double time = 0.0;
            double passedTime = 0.0;

            while (running) {
                time = System.currentTimeMillis();
                grid.update();
                grid.paint(context);

                if (grid.getSnake().isDead())
                    running = false;


                printScore();
                passedTime = System.currentTimeMillis() - time;
                if (passedTime < settings.MS_PER_UPDATE) {
                    try {
                        Thread.sleep((long) (settings.MS_PER_UPDATE - passedTime));
                    } catch (InterruptedException ignored) {
                    }
                }
            }
            System.out.println(running);
            System.out.println("End child");
            notify();
        }

    }
    public boolean getRunning(){return running;}

    private void printScore() {
        context.setFill(Color.BEIGE);
        context.fillText("Score: " + (grid.getSnake().length() - 1) * 10, 10,
                context.getCanvas().getHeight() - 10);
    }


}
