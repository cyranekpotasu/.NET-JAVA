package snake;

import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;

public class GameLoop implements Runnable {
    private final GameController controller;
    private final GraphicsContext context;
    private final Grid grid;
    private boolean running = false;
    private final double MS_PER_UPDATE = 1000.0 / 60.0;

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
        running = true;

        double time = 0.0;
        double passedTime = 0.0;

        while (running) {
            time = System.currentTimeMillis();
            grid.update();
            grid.paint(context);
            passedTime = System.currentTimeMillis() - time;
            if (passedTime < MS_PER_UPDATE) {
                try {
                    Thread.sleep((long) (MS_PER_UPDATE - passedTime));
                } catch (InterruptedException ignored) {
                }
            }
        }
    }
}
