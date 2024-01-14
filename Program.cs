using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RanSanTien
{
    class Program
    {
        static int chieuRongHangRao = 80;
        static int chieuCaoHangRao = 25;
        static int score = 0; // Điểm số
        static int moneyX; // Tọa độ X của tiền
        static int moneyY; // Tọa độ Y của tiền
        static char money = '$'; // icon của tiền
        static bool ketThuc = false; // Biến kiểm tra trạng thái kết thúc trò chơi
        static Random random = new Random();

        static List<int> snakeX = new List<int>(); // Danh sách lưu tọa độ X của con rắn
        static List<int> snakeY = new List<int>(); // Danh sách lưu tọa độ Y của con rắn
        
        static char snake = 'O'; // icon vẽ con rắn
        static int headX; // Tọa độ X của đầu rắn
        static int headY; // Tọa độ Y của đầu rắn
        static int diChuyen; // Hướng di chuyển của rắn (0: lên, 1: xuống, 2: trái, 3: phải)

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            
            // Thiết lập cửa sổ console
            Console.WindowWidth = 85;
            Console.WindowHeight = 30;

            Console.CursorVisible = false; // Ẩn con trỏ

            // Khởi tạo con rắn
            DrawSnake();

            // Khởi tạo icon tiền
            GenerateMoney();

            // Vòng lặp chính của trò chơi
            while (!ketThuc)
            {
                // Vẽ màn hình và các đối tượng
                Draw();

                // Nhận phím nhập từ người chơi
                Control();

                // Cập nhật trạng thái của con rắn
                Update();
                Thread.Sleep(100);
            }
            
            // Kết thúc trò chơi
            Console.Clear();
            Console.SetCursorPosition(chieuRongHangRao / 2 - 5, chieuCaoHangRao / 2);
            Console.WriteLine("Game Over!");
            Console.SetCursorPosition(chieuRongHangRao / 2 - 9, chieuCaoHangRao / 2 + 1);
            Console.WriteLine("Điểm của bạn là:" + score + "\n");
        }

        // Vẽ con rắn
        static void DrawSnake()
        {
            // Ban đầu con rắn có độ dài là 2
            // Tạo ra một con rắn có dạng ** ở giữa bảng

            // Vị trí xuất phát của đầu con rắn
            snakeX.Add(chieuRongHangRao / 2);
            snakeY.Add(chieuCaoHangRao / 2);

            snakeX.Add((chieuRongHangRao / 2) - 1);
            snakeY.Add(chieuCaoHangRao / 2);

            snakeX.Add((chieuRongHangRao / 2) - 2);
            snakeY.Add(chieuCaoHangRao / 2);

            // Đặt đầu rắn ở vị trí đầu tiên của danh sách
            headX = snakeX[0];
            headY = snakeY[0];

            // Đặt hướng di chuyển ban đầu là bên phải
            // Hướng di chuyển của rắn (0: lên, 1: xuống, 2: trái, 3: phải)
            diChuyen = 3;
        }

        // Random vị trí của tiền
        static void GenerateMoney()
        {
            // Sinh tọa độ X và Y ngẫu nhiên trong phạm vi của hàng rào
            moneyX = random.Next(1, chieuRongHangRao - 1);
            moneyY = random.Next(1, chieuCaoHangRao - 1);

            // Kiểm tra xem tọa độ của tiền có trùng với con rắn hay không (rắn đã ăn tiền chưa)
            // Nếu có thì random chỗ khác
            for (int i = 0; i < snakeX.Count; i++)
            {
                if (moneyX == snakeX[i] && moneyY == snakeY[i])
                {
                    GenerateMoney();
                    break;
                }
                if (
                    ((moneyX >= 4 && moneyX <= 9) && moneyY == 5) ||
                    ((moneyX >= 8 && moneyX <= 13) && moneyY == 19) ||
                    ((moneyX >= 14 && moneyX <= 19) && moneyY == 10) ||
                    ((moneyX >= 24 && moneyX <= 29) && moneyY == 15) ||
                    ((moneyX >= 34 && moneyX <= 39) && moneyY == 20) ||
                    ((moneyX >= 64 && moneyX <= 69) && moneyY == 7) ||
                    ((moneyX >= 44 && moneyX <= 49) && moneyY == 14) ||
                    ((moneyX >= 46 && moneyX <= 51) && moneyY == 3) ||
                    ((moneyX >= 33 && moneyX <= 38) && moneyY == 6) ||
                    ((moneyX >= 60 && moneyX <= 65) && moneyY == 20)
                   )
                {
                    GenerateMoney();
                    break;
                }
            }
        }

        // Phương thức vẽ hàng rào và các đối tượng
        static void Draw()
        {
            // Vẽ khung hàng rào
            for (int i = 0; i < chieuRongHangRao; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
                Console.SetCursorPosition(i, chieuCaoHangRao - 1);
                Console.Write("#");
            }
            for (int i = 0; i < chieuCaoHangRao; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
                Console.SetCursorPosition(chieuRongHangRao, i);
                Console.Write("#");
            }

            // Vẽ 10 bức tường làm vật cản
            for (int i = 4; i < 10; i++)
            {
                Console.SetCursorPosition(i, 5);
                Console.Write("+");
            }
            for (int i = 14; i < 20; i++)
            {
                Console.SetCursorPosition(i, 10);
                Console.Write("+");
            }
            for (int i = 24; i < 30; i++)
            {
                Console.SetCursorPosition(i, 15);
                Console.Write("+");
            }
            for (int i = 34; i < 40; i++)
            {
                Console.SetCursorPosition(i, 20);
                Console.Write("+");
            }
            for (int i = 64; i < 70; i++)
            {
                Console.SetCursorPosition(i, 7);
                Console.Write("+");
            }
            for (int i = 44; i < 50; i++)
            {
                Console.SetCursorPosition(i, 14);
                Console.Write("+");
            }
            for (int i = 46; i < 52; i++)
            {
                Console.SetCursorPosition(i, 3);
                Console.Write("+");
            }
            for (int i = 33; i < 39; i++)
            {
                Console.SetCursorPosition(i, 6);
                Console.Write("+");
            }
            for (int i = 8; i < 14; i++)
            {
                Console.SetCursorPosition(i, 19);
                Console.Write("+");
            }
            for (int i = 60; i < 66; i++)
            {
                Console.SetCursorPosition(i, 20);
                Console.Write("+");
            }

            // Vẽ icon tiền
            Console.SetCursorPosition(moneyX, moneyY);
            Console.Write(money);

            // Vẽ con rắn
            for (int i = 0; i < snakeX.Count; i++)
            {
                Console.SetCursorPosition(snakeX[i], snakeY[i]);
                Console.Write(snake);
            }

            // Vẽ điểm số
            Console.SetCursorPosition((chieuRongHangRao / 2) - 9, chieuCaoHangRao);
            Console.Write("Điểm của bạn là: " + score);
        }

        // Phương thức nhận phím nhập từ người chơi
        static void Control()
        {
            // Nếu có phím được nhấn
            if (Console.KeyAvailable)
            {
                // Lấy phím nhấn
                ConsoleKey key = Console.ReadKey(true).Key;

                // Đổi hướng di chuyển của rắn tương ứng với phím nhấn
                // Chỉ đổi hướng nếu phím nhấn không trùng với hướng hiện tại hoặc hướng ngược lại
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (diChuyen != 0 && diChuyen != 1)
                            diChuyen = 0;
                        break;

                    case ConsoleKey.DownArrow:
                        if (diChuyen != 0 && diChuyen != 1)
                            diChuyen = 1;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (diChuyen != 2 && diChuyen != 3)
                            diChuyen = 2;
                        break;

                    case ConsoleKey.RightArrow:
                        if (diChuyen != 2 && diChuyen != 3)
                            diChuyen = 3;
                        break;
                }
            }
        }

        // Cập nhật trạng thái của con rắn
        static void Update()
        {
            // Cập nhật tọa độ của đầu rắn theo hướng di chuyển
            // Sử dụng một câu lệnh switch-case để kiểm tra hướng di chuyển và tăng hoặc giảm tọa độ X hoặc Y tương ứng
            switch (diChuyen)
            {
                case 0:
                    headY--;
                    break;
                case 1:
                    headY++;
                    break;
                case 2:
                    headX--;
                    break;
                case 3:
                    headX++;
                    break;
            }

            // Đặt con trỏ ở vị trí cuối cùng của rắn và ghi một khoảng trắng để xóa ký tự rắn cũ
            Console.SetCursorPosition(snakeX[snakeX.Count - 1], snakeY[snakeY.Count - 1]);
            Console.Write(" ");

            // Kiểm tra xem đầu rắn có va chạm vào khung hàng rào không
            // Nếu có thì kết thúc trò chơi
            if (headX <= 0 || headX >= chieuRongHangRao - 1 || headY <= 0 || headY >= chieuCaoHangRao - 1)
            {
                ketThuc = true;
                return;
            }

            // Kiểm tra xem đầu rắn có va chạm vào vật cản không
            // Nếu có thì kết thúc trò chơi
            if (
                ((headX >= 4 && headX <= 9) && headY == 5) ||
                ((headX >= 8 && headX <= 13) && headY == 19) ||
                ((headX >= 14 && headX <= 19) && headY == 10) ||
                ((headX >= 24 && headX <= 29) && headY == 15) ||
                ((headX >= 34 && headX <= 39) && headY == 20) ||
                ((headX >= 64 && headX <= 69) && headY == 7) ||
                ((headX >= 44 && headX <= 49) && headY == 14) ||
                ((headX >= 46 && headX <= 51) && headY == 3) ||
                ((headX >= 33 && headX <= 38) && headY == 6) ||
                ((headX >= 60 && headX <= 65) && headY == 20)
               )
            {
                ketThuc = true;
                return;
            }
                       
            // Kiểm tra xem đầu rắn có va chạm vào thân rắn không
            // Nếu có thì kết thúc trò chơi
            for (int i = 1; i < snakeX.Count; i++)
            {
                if (headX == snakeX[i] && headY == snakeY[i])
                {
                    ketThuc = true;
                    return;
                }
            }

            // Kiểm tra xem đầu rắn có ăn tiền không
            // Nếu có thì tăng điểm số, tăng độ dài của rắn và tạo vị trí tiền mới
            if (headX == moneyX && headY == moneyY)
            {
                score++;
                snakeX.Add(0);
                snakeY.Add(0);
                GenerateMoney();
            }

            // Cập nhật tọa độ của thân rắn theo đầu rắn
            // Bắt đầu từ phần cuối của rắn, gán tọa độ của mỗi phần bằng tọa độ của phần trước nó
            for (int i = snakeX.Count - 1; i > 0; i--)
            {
                snakeX[i] = snakeX[i - 1];
                snakeY[i] = snakeY[i - 1];
            }

            // Gán tọa độ của phần đầu của rắn bằng tọa độ mới của đầu rắn
            snakeX[0] = headX;
            snakeY[0] = headY;

            // Xóa con trỏ cũ
            Console.SetCursorPosition(snakeX[snakeX.Count - 1], snakeY[snakeY.Count - 1]);
            Console.Write(" ");

            // Vẽ con trỏ mới
            Console.SetCursorPosition(headX, headY);
            Console.Write(snake);
        }
    }
}