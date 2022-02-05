using System;
using System.Numerics;
using Raylib_cs;

namespace AttackThing
{
    class Attack
    {
       public string name;
       public int damage;
    }

    class Program
    {
        Vector2 m_windowRatio = new Vector2(16, 9);

        int m_windowSize = 120;
        int m_1080p = 120;
        int m_4k = 256;
        string m_windowName = "AttackTest";

        Random m_random = new Random();
        bool m_haveSameAttack = false;

        List<Attack> m_attacks = new List<Attack>();
        List<Attack> m_availableAttacks = new List<Attack>();

        static void Main(string[] args)
        {
            Program p = new Program();

            p.RunProgram();
        }

        void RunProgram()
        {
            m_windowSize = m_1080p;

            Raylib.InitWindow((int)m_windowRatio.X * m_windowSize, (int)m_windowRatio.Y * m_windowSize, m_windowName);
            //Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE);
            Raylib.SetTargetFPS(60);

            LoadGame();

            while (!Raylib.WindowShouldClose())
            {
                Update();
                Draw();
            }

            Raylib.WindowShouldClose();
        }

        void LoadGame()
        {
            // examples
            // add attacks here
            Console.WriteLine("Loading Attacks");
            m_attacks.Add(new Attack() { name = "Elbow", damage = 10 });
            m_attacks.Add(new Attack() { name = "Kick", damage = 12 });
            m_attacks.Add(new Attack() { name = "Knee", damage = 8 });
            m_attacks.Add(new Attack() { name = "Head Butt", damage = 6 });
            m_attacks.Add(new Attack() { name = "Round House", damage = 15 });
            m_attacks.Add(new Attack() { name = "Hook", damage = 12 });
            Console.WriteLine("Attacks Loaded");
        }

        void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ONE))
            {
                m_haveSameAttack = !m_haveSameAttack;
                Console.WriteLine("Same Attacks: " + m_haveSameAttack);
            }
               
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                Console.WriteLine("NewAttacks");
                m_availableAttacks.Clear();

                // goes through until 3 attacks in available attacks
                while (m_availableAttacks.Count < 3)
                {
                    int randAttack = m_random.Next(m_attacks.Count);

                    // allows multiple of an attack
                    if (m_haveSameAttack)
                    {
                        m_availableAttacks.Add(m_attacks[randAttack]);
                    }
                    // each attack is different
                    else
                    {
                        if (m_availableAttacks.Count() == 0)
                            m_availableAttacks.Add(m_attacks[randAttack]);
                        else
                        {
                            for (int j = 0; j < m_availableAttacks.Count;)
                            {
                                if (m_attacks[randAttack] != m_availableAttacks[j])
                                {
                                    j++;
                                    if (j == m_availableAttacks.Count)
                                        m_availableAttacks.Add(m_attacks[randAttack]);
                                }
                                else
                                    j = m_availableAttacks.Count;
                            }
                        }
                    }
                }
            }
        }

        void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RAYWHITE);

            Raylib.DrawCircleLines((int)(0.6f * m_windowSize), (int)(0.6f * m_windowSize), 0.5f * m_windowSize, Color.BLUE);
            Raylib.DrawCircleLines((int)(2f * m_windowSize), (int)(0.6f * m_windowSize), 0.5f * m_windowSize, Color.BLUE);
            Raylib.DrawCircleLines((int)(3.4f * m_windowSize), (int)(0.6f * m_windowSize), 0.5f * m_windowSize, Color.BLUE);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                int i = 0;
            }

            if (m_availableAttacks.Count == 3)
            {
                Raylib.DrawText(m_availableAttacks[0].name, (int)(0.15f * m_windowSize), (int)(1.2f * m_windowSize), (int)(0.18f * m_windowSize), Color.BLACK);
                Raylib.DrawText(m_availableAttacks[1].name, (int)(1.55f * m_windowSize), (int)(1.2f * m_windowSize), (int)(0.18f * m_windowSize), Color.BLACK);
                Raylib.DrawText(m_availableAttacks[2].name, (int)(2.95f * m_windowSize), (int)(1.2f * m_windowSize), (int)(0.18f * m_windowSize), Color.BLACK);
            }

            string isEnabledIndicator;

            string enabled = "Enabled";
            string disabled = "Disabled";

            if (m_haveSameAttack)
                isEnabledIndicator = enabled;
            else
                isEnabledIndicator = disabled;

            Raylib.DrawText(("Unique Attacks: " + isEnabledIndicator), (int)(0.15f * m_windowSize), (int)(8.5f * m_windowSize), (int)(0.3f * m_windowSize), Color.BLACK);

            Raylib.EndDrawing();
        }
    }
}