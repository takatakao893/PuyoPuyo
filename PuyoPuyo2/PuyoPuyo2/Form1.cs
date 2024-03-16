using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace PuyoPuyo2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Field.Paint += Field_Paint;
            Field.KeyDown += Field_KeyDown;

            this.BackColor = Color.Black;
            Field.BorderStyle = BorderStyle.None;

            label1.ForeColor = Color.White;
            label1.Font = new Font("MS �S�V�b�N", 12, FontStyle.Bold);

            // �^�C�}�[����
            this.timer1.Interval = 800;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

            Score = 0;

            GetPuyoImages();
            InitCells();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PuyoMoveDownIfCan();
        }

        // �^�C�}�[
        Timer timer1 = new Timer();
        // ����
        Random r1 = new Random();
        // �Z���̔z��
        Cell[,] Cells = null;

        // �X�R�A
        int score = 0;

        // �t�B�[���h�̕��ƍ���
        int FIELD_WIDTH = 6 + 2;
        int FIELD_HEIGHT = 12 + 1 + 1;

        // �g�Ղ�̉�]���ɂȂ鑤�̈ʒu
        int puyoPositionX = puyoStartPositionX;
        int puyoPositionY = puyoStartPositionY;

        // �g�Ղ悪�ŏ��ɂ�����镔���̈ʒu
        const int puyoStartPositionX = 3;
        const int puyoStartPositionY = 1;

        bool IsGameOver = true;
        // �������̓L�[����𖳌��ɂ���
        bool isIgnoreKeyDown = false;

        // �g�Ղ�͂ǂꂾ����]�������H
        Angle PuyoAngle = Angle.Angle0;
        Puyo DropingPuyo = Puyo.Puyo1;
        Puyo SubDropingPuyo = Puyo.Puyo2;

        Image redPuyoImage = null;
        Image bluePuyoImage = null;
        Image greenPuyoImage = null;
        Image yellowPuyoImage = null;
        Image wallImage = null;

        List<Image> redImages = new List<Image>();
        List<Image> greenImages = new List<Image>();
        List<Image> blueImages = new List<Image>();
        List<Image> yellowImages = new List<Image>();

        public enum Angle
        {
            Angle0 = 0,
            Angle90 = 1,
            Angle180 = 2,
            Angle270 = 3,
        }

        void InitCells()
        {
            Cells = new Cell[FIELD_HEIGHT, FIELD_WIDTH];

            int selWidth = Cell.Size.Width;
            int selHeight = Cell.Size.Height;

            for (int i = 0; i < FIELD_HEIGHT; i++)
            {
                for (int j = 0; j < FIELD_WIDTH; j++)
                {
                    // (i - 1)�Ƃ��邱�Ƃň�ԏ�̍s��
                    // �R���g���[���̊O���ɂȂ�\������Ȃ�
                    Cells[i, j] = new Cell(j * selWidth, (i - 1) * selHeight);
                    Cells[i, j].SetColumRow(j, i);
                }
            }

            // �O�g��\��������
            for (int i = 0; i < FIELD_HEIGHT; i++)
                SetPuyo(0, i, Puyo.Wall);
            for (int i = 0; i < FIELD_HEIGHT; i++)
                SetPuyo(FIELD_WIDTH - 1, i, Puyo.Wall);
            for (int i = 0; i < FIELD_WIDTH; i++)
                SetPuyo(i, FIELD_HEIGHT - 1, Puyo.Wall);
        }

        void SetPuyo(int colum, int row, Puyo puyo)
        {
            Cell cell = Cells[row, colum];
            cell.Puyo = puyo;
            Field.Invalidate();
        }

        int Score
        {
            get { return score; }
            set
            {
                score = value;
                label1.Text = "Score" + score.ToString();
            }
        }

        void GetPuyoImages()
        {
            redPuyoImage = Properties.Resources.Red;
            bluePuyoImage = Properties.Resources.Blue;
            greenPuyoImage = Properties.Resources.Green;
            yellowPuyoImage = Properties.Resources.Yellow;
            wallImage = Properties.Resources.Wall;

            redImages.Add(Properties.Resources.rn01);
            redImages.Add(Properties.Resources.rn02);
            redImages.Add(Properties.Resources.rn03);
            redImages.Add(Properties.Resources.rn04);
            redImages.Add(Properties.Resources.rn05);
            redImages.Add(Properties.Resources.rn06);
            redImages.Add(Properties.Resources.rn07);
            redImages.Add(Properties.Resources.rn08);
            redImages.Add(Properties.Resources.rn09);
            redImages.Add(Properties.Resources.rn10);
            redImages.Add(Properties.Resources.rn11);
            redImages.Add(Properties.Resources.rn12);
            redImages.Add(Properties.Resources.rn13);
            redImages.Add(Properties.Resources.rn14);
            redImages.Add(Properties.Resources.rn15);
            redImages.Add(Properties.Resources.rn16);
            redImages.Add(Properties.Resources.rn17);
            redImages.Add(Properties.Resources.rn18);
            redImages.Add(Properties.Resources.rn19);

            greenImages.Add(Properties.Resources.gn01);
            greenImages.Add(Properties.Resources.gn02);
            greenImages.Add(Properties.Resources.gn03);
            greenImages.Add(Properties.Resources.gn04);
            greenImages.Add(Properties.Resources.gn05);
            greenImages.Add(Properties.Resources.gn06);
            greenImages.Add(Properties.Resources.gn07);
            greenImages.Add(Properties.Resources.gn08);
            greenImages.Add(Properties.Resources.gn09);
            greenImages.Add(Properties.Resources.gn10);
            greenImages.Add(Properties.Resources.gn11);
            greenImages.Add(Properties.Resources.gn12);
            greenImages.Add(Properties.Resources.gn13);
            greenImages.Add(Properties.Resources.gn14);
            greenImages.Add(Properties.Resources.gn15);
            greenImages.Add(Properties.Resources.gn16);
            greenImages.Add(Properties.Resources.gn17);
            greenImages.Add(Properties.Resources.gn18);
            greenImages.Add(Properties.Resources.gn19);

            blueImages.Add(Properties.Resources.bn01);
            blueImages.Add(Properties.Resources.bn02);
            blueImages.Add(Properties.Resources.bn03);
            blueImages.Add(Properties.Resources.bn04);
            blueImages.Add(Properties.Resources.bn05);
            blueImages.Add(Properties.Resources.bn06);
            blueImages.Add(Properties.Resources.bn07);
            blueImages.Add(Properties.Resources.bn08);
            blueImages.Add(Properties.Resources.bn09);
            blueImages.Add(Properties.Resources.bn10);
            blueImages.Add(Properties.Resources.bn11);
            blueImages.Add(Properties.Resources.bn12);
            blueImages.Add(Properties.Resources.bn13);
            blueImages.Add(Properties.Resources.bn14);
            blueImages.Add(Properties.Resources.bn15);
            blueImages.Add(Properties.Resources.bn16);
            blueImages.Add(Properties.Resources.bn17);
            blueImages.Add(Properties.Resources.bn18);
            blueImages.Add(Properties.Resources.bn19);

            yellowImages.Add(Properties.Resources.yn01);
            yellowImages.Add(Properties.Resources.yn02);
            yellowImages.Add(Properties.Resources.yn03);
            yellowImages.Add(Properties.Resources.yn04);
            yellowImages.Add(Properties.Resources.yn05);
            yellowImages.Add(Properties.Resources.yn06);
            yellowImages.Add(Properties.Resources.yn07);
            yellowImages.Add(Properties.Resources.yn08);
            yellowImages.Add(Properties.Resources.yn09);
            yellowImages.Add(Properties.Resources.yn10);
            yellowImages.Add(Properties.Resources.yn11);
            yellowImages.Add(Properties.Resources.yn12);
            yellowImages.Add(Properties.Resources.yn13);
            yellowImages.Add(Properties.Resources.yn14);
            yellowImages.Add(Properties.Resources.yn15);
            yellowImages.Add(Properties.Resources.yn16);
            yellowImages.Add(Properties.Resources.yn17);
            yellowImages.Add(Properties.Resources.yn18);
            yellowImages.Add(Properties.Resources.yn19);
        }

        Image GetImageFromPuyoType(Puyo puyo)
        {
            if (puyo == Puyo.Puyo1)
                return redPuyoImage;
            else if (puyo == Puyo.Puyo2)
                return bluePuyoImage;
            else if (puyo == Puyo.Puyo3)
                return greenPuyoImage;
            else if (puyo == Puyo.Puyo4)
                return yellowPuyoImage;
            else if (puyo == Puyo.Wall)
                return wallImage;

            return null;
        }

        Image GetImageFromPuyoTypeRensa(Puyo puyo, int rensa)
        {
            if(puyo == Puyo.Puyo1)
            {
                if (rensa < redImages.Count)
                    return redImages[rensa - 1];
                else
                    return redImages[redImages.Count - 1];
            }
            if(puyo == Puyo.Puyo2)
            {
                if (rensa < blueImages.Count)
                    return blueImages[rensa - 1];
                else 
                    return blueImages[blueImages.Count - 1];
            }
            if(puyo == Puyo.Puyo3)
            {
                if(rensa < greenImages.Count)
                    return greenImages[rensa - 1];
                else
                    return greenImages[greenImages.Count - 1];
            }
            if(puyo == Puyo.Puyo4)
            {
                if(rensa < yellowImages.Count)
                    return yellowImages[rensa - 1]; 
                else
                    return yellowImages[yellowImages.Count - 1];
            }
            return null;
        }

        private void Field_Paint(object sender, PaintEventArgs e)
        {
            DrawPuyo(e.Graphics);
        }
        
        private void Field_KeyDown(object sender, KeyEventArgs e)
        {
            if(isIgnoreKeyDown) 
                return;

            if (IsGameOver)
                return;

            if (e.KeyCode == Keys.Left)
                PuyoMoveLeftIfCan();
            if (e.KeyCode == Keys.Right)
                PuyoMoveRightIfCan();
            if (e.KeyCode == Keys.Down)
                PuyoMoveDownIfCan();
            if (e.KeyCode == Keys.Space)
                PuyoRotateIfCan();
        }

        async void OnFixed()
        {
            // �������̓L�[���삪�ł��Ȃ��悤�ɂ���
            isIgnoreKeyDown = true;
            timer1.Stop();

            int rensa = 1;
            int tempScore = 0;  // ���Z�����_��

            while (true)
            {
                Task<int> task = DeletePuyoIfNeed(rensa);
                int ret = await task;
                if (ret == 0)
                    break;

                tempScore += ret;
                rensa++;
            }

            Score += tempScore; // �_�������Z

            isIgnoreKeyDown = false;
            timer1.Start();

            // �������I�������V�����Ղ���~�点��
            CreateNewPuyo();

        }

        async Task<int> DeletePuyoIfNeed(int rensa)
        {
            // �ݒu���ꂽ�Ղ�̉��ɋ󓴂�����Ώ�̂Ղ�𗎂Ƃ��ċ󓴂����߂�
            Task<bool> task = DownPuyoIfSpaces();
            await task;

            List<Cell> deleteCells = new List<Cell>();

            // �A���{�[�i�X�v�Z�p
            List<int> vs = new List<int>();

            // 4�Ȃ����Ă���Ղ悪�����������
            for (int i = 0; i < FIELD_HEIGHT; i++)
            {
                for (int j = 0; j < FIELD_WIDTH; j++)
                {
                    Cell cell = Cells[i, j];
                    if (deleteCells.Any(x => x == cell))
                        continue;

                    // �A���Ɠ��������͋�ʂ��邽�ߕʂ̕ϐ��Ɋi�[����
                    List<Cell> cells = GetConnectedCell(cell);
                    ClearCheck();

                    if (cells.Count >= 4)
                    {
                        deleteCells.AddRange(cells);
                        vs.Add(cells.Count);

                        // ������Ղ�ɘA���񐔂��킩��悤�ɔԍ�������
                        Puyo puyo = cells[0].Puyo;
                        Image image = GetImageFromPuyoTypeRensa(puyo, rensa);

                        Graphics g = Graphics.FromHwnd(Field.Handle);
                        foreach(Cell cell1 in cells)
                        {
                            Rectangle rect = new Rectangle(cell1.LeftTop, Cell.Size);
                            g.DrawImage(image, rect);
                        }
                        g.Dispose();
                    }
                }
            }

            // ���_�́u�Ղ�̏������� * (�A���{�[�i�X+�A���{�[�i�X+�F���{�[�i�X) * 10�v

            // �Ղ�̏�������
            int puyoCount = deleteCells.Count;

            // �A���{�[�i�X (��q)
            int rensaBonus = GetRensaBonus(rensa);

            // �A���{�[�i�X (��q)
            int renketsuBonus = vs.Sum(x => GetRenketsuBonus(x));

            // �F���{�[�i�X�i��q�j
            int colorsKind = deleteCells.Distinct(new PuyoEqualityComparer()).Count();
            int colorsBonus = GetColorCountBonus(colorsKind);

            int score = 0;
            if (rensaBonus + renketsuBonus + colorsBonus != 0)
                score = puyoCount * (rensaBonus + renketsuBonus + colorsBonus) * 10;
            else
                score = puyoCount * 10;

            // �Ղ�����ۂɏ���
            foreach(Cell cell0 in deleteCells)
            {
                cell0.Puyo = Puyo.None;
            }
            if (deleteCells.Count != 0)
            {
                // �Ղ旎����
                await Task.Delay(1000);

                Field.Invalidate();
            }

            return score;
        }

        // �A���{�[�i�X�̌v�Z
        int GetRensaBonus(int i)
        {
            if (i == 1)
                return 0;
            if (i == 2)
                return 8;
            if (i == 3)
                return 16;
            if (i >= 4 || i <= 19)
                return (i - 3) * 32;
            if (i > 19)
                return 512;
            return 0;
        }

        // �A���{�[�i�X�̌v�Z
        int GetRenketsuBonus(int i)
        {
            if (i == 4)
                return 0;
            if (i >= 5 || i <= 10)
                return i - 3;
            if (i < 11)
                return 10;
            return 0;
        }

        int GetColorCountBonus(int i)
        {
            if (i == 1)
                return 0;
            if (i == 2)
                return 3;
            if (i == 3)
                return 6;
            if (i == 4)
                return 12;
            if (i == 5)
                return 24;
            return 0;
        }

        List<Cell> GetConnectedCell(Cell cell)
        {
            int colum = cell.Colum;
            int row = cell.Row;

            List<Cell> cells = new List<Cell>();

            if(0 < colum && colum < FIELD_WIDTH - 1 && 0 < row && row < FIELD_HEIGHT - 1)
            {
                Puyo puyo = Cells[row, colum].Puyo;
                if (puyo == Puyo.None || puyo == Puyo.Wall)
                    return cells;

                if (!Cells[row+1, colum].isChecked)
                {
                    Cells[row + 1, colum].isChecked = true;
                    if (Cells[row + 1, colum].Puyo == puyo)
                    {
                        cells.Add(Cells[row + 1, colum]);
                        cells.AddRange(GetConnectedCell(Cells[row + 1, colum]));
                    }
                }

                if (!Cells[row - 1, colum].isChecked)
                {
                    Cells[row - 1, colum].isChecked = true;
                    if (Cells[row - 1, colum].Puyo == puyo)
                    {
                        cells.Add(Cells[row - 1, colum]);
                        cells.AddRange(GetConnectedCell(Cells[row - 1, colum]));
                    }
                }

                if (!Cells[row, colum + 1].isChecked)
                {
                    Cells[row, colum + 1].isChecked = true;
                    if (Cells[row, colum + 1].Puyo == puyo)
                    {
                        cells.Add(Cells[row, colum + 1]);
                        cells.AddRange(GetConnectedCell(Cells[row, colum + 1]));
                    }
                }

                if (!Cells[row, colum - 1].isChecked)
                {
                    Cells[row, colum - 1].isChecked = true;
                    if (Cells[row, colum - 1].Puyo == puyo)
                    {
                        cells.Add(Cells[row, colum - 1]);
                        cells.AddRange(GetConnectedCell(Cells[row, colum - 1]));
                    }
                }
            }
            return cells;
        }

        void ClearCheck()
        {
            for(int i=0; i<FIELD_HEIGHT; i++)
            {
                for(int j=0; j<FIELD_WIDTH; j++)
                {
                    Cell cell = Cells[i, j];
                    cell.isChecked = false;
                }
            }
        }

        Position GetSubPuyoPosition(int mainColun, int mainRow, Angle angle)
        {
            if (angle == Angle.Angle0)
                return new Position(mainColun, mainRow - 1);
            if (angle == Angle.Angle90)
                return new Position(mainColun + 1, mainRow);
            if (angle == Angle.Angle180)
                return new Position(mainColun, mainRow + 1);
            if (angle == Angle.Angle270)
                return new Position(mainColun - 1, mainRow);

            return null;
        }

        void DrawPuyo(Graphics g)
        {
            Field.BackColor = Color.Black;
            for (int i = 0; i < FIELD_HEIGHT; i++)
            {
                for (int j = 0; j < FIELD_WIDTH; j++)
                {
                    Cell cell = Cells[i, j];
                    Rectangle rect = new Rectangle(cell.LeftTop, Cell.Size);

                    Image image = GetImageFromPuyoType(cell.Puyo);
                    if (image != null)
                        g.DrawImage(image, rect);
                }
            }
        }

        void CheckGameOver()
        {
            if (Cells[puyoPositionY, puyoPositionX].IsFixed)
            {
                timer1.Stop();
                IsGameOver = true;
            }
        }

        private void GameStartMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsGameOver)
                return;

            IsGameOver = false;
            InitCells();

            CreateNewPuyo();
            timer1.Start();
        }

        void CreateNewPuyo()
        {
            PuyoAngle = Angle.Angle0;

            int r = r1.Next(0, 4);
            if (r == 0)
                DropingPuyo = Puyo.Puyo1;
            if (r == 1)
                DropingPuyo = Puyo.Puyo2;
            if (r == 2)
                DropingPuyo = Puyo.Puyo3;
            if (r == 3)
                DropingPuyo = Puyo.Puyo4;

            r = r1.Next(0, 4);
            if (r == 0)
                SubDropingPuyo = Puyo.Puyo1;
            if (r == 1)
                SubDropingPuyo = Puyo.Puyo2;
            if (r == 2)
                SubDropingPuyo = Puyo.Puyo3;
            if (r == 3)
                SubDropingPuyo = Puyo.Puyo4;

            puyoPositionX = puyoStartPositionX;
            puyoPositionY = puyoStartPositionY;
            SetPuyo(puyoPositionX, puyoPositionY, DropingPuyo);
            SetPuyo(puyoPositionX, puyoPositionY - 1, SubDropingPuyo);

            CheckGameOver();
        }

        void PuyoMoveDownIfCan()
        {
            int oldX = puyoPositionX;
            int oldY = puyoPositionY;

            Position subPosition = GetSubPuyoPosition(puyoPositionX, puyoPositionY, PuyoAngle);
            int oldSubX = subPosition.Colum;
            int oldSubY = subPosition.Row;

            // �ړ��ł���̂��H
            int newX = puyoPositionX;
            int newY = puyoPositionY + 1;
            int newSubX = oldSubX;
            int newSubY = subPosition.Row + 1;

            // �������Ȃ��Ȃ�Œ肷��
            if (!CanMoveDown(new Position(newX, newY)) || !CanMoveDown(new Position(newSubX, newSubY)))
            {
                FixPuyo(oldX, oldY);
                FixPuyo(oldSubX, oldSubY);
                OnFixed();
                return;
            }

            SetPuyo(oldX, oldY, Puyo.None);
            SetPuyo(oldSubX, oldSubY, Puyo.None);

            puyoPositionY = newY;
            SetPuyo(puyoPositionX, puyoPositionY, DropingPuyo);
            SetPuyo(newSubX, newSubY, SubDropingPuyo);
        }

        void FixPuyo(int colum, int row)
        {
            Cells[row, colum].IsFixed = true;
        }

        async Task<bool> DownPuyoIfSpaces()
        {
            bool ret = false;
            // �Ղ�̒����ɋ󓴂��������A��̂Ղ�����ɉ�����
            while(DownPuyoIfSpace())
            {
                ret = true;

                //��C�ɉ�����̂ł͂Ȃ�1�i����0.1�b���Ƃɉ�����
                await Task.Delay(100);
            }
            return ret;
        }

        bool DownPuyoIfSpace()
        {
            // ��x�ł������������Ȃ�ꂽ�ꍇ��true��Ԃ�
            bool ret = false;
            for(int y = FIELD_HEIGHT - 3; y >= 0; y--)
            {
                for(int x = 0; x < FIELD_WIDTH; x++)
                {
                    if (Cells[y+1, x].Puyo == Puyo.None && Cells[y, x].Puyo != Puyo.None)
                    {
                        // ���ɋ�Ԃ�����Ղ�͉��ɗ��Ƃ�
                        SetPuyo(x, y + 1, Cells[y, x].Puyo);
                        Cells[y + 1, x].IsFixed = Cells[y, x].IsFixed;

                        SetPuyo(x, y, Puyo.None);
                        Cells[y, x].IsFixed = false;

                        ret = true;
                    }
                }
            }
            if (ret)
                Field.Invalidate();
            return ret;
        }

        bool CanMoveDown(Position newPos)
        {
            if(
                newPos.Row < 0 || newPos.Row > FIELD_HEIGHT - 2 ||
                Cells[newPos.Row, newPos.Colum].IsFixed)
                return false;
            else
                return true;
        }

        bool CanMove(Position newPos)
        {
            if(
                newPos.Colum < 0 || newPos.Colum > FIELD_WIDTH - 1 ||
                newPos.Row < 0 || newPos.Row > FIELD_HEIGHT - 1 ||
                Cells[newPos.Row, newPos.Colum].IsFixed ||
                Cells[newPos.Row, newPos.Colum].Puyo == Puyo.Wall
            )
                return false;
            else
                return true;
        }

        void PuyoRotateIfCan()
        {
            Position oldSubPos = GetSubPuyoPosition(puyoPositionX, puyoPositionY, PuyoAngle);

            Angle angle = Angle.Angle0;
            if (PuyoAngle == Angle.Angle0)
                angle = Angle.Angle90;
            else if (PuyoAngle == Angle.Angle90)
                angle = Angle.Angle180;
            else if (PuyoAngle == Angle.Angle180)
                angle = Angle.Angle270;
            else if (PuyoAngle == Angle.Angle270)
                angle = Angle.Angle0;

            Position newSubPos = GetSubPuyoPosition(puyoPositionX, puyoPositionY, angle);
            if(CanMove(newSubPos))
            {
                // ��]�p�x���t�B�[���h�ϐ��ɃZ�b�g
                PuyoAngle = angle;

                // ���Ƃ̈ʒu�̃Z���́u�Ղ�Ȃ��v�̏�Ԃɂ��āA�V�����ʒu�ɂՂ���Z�b�g����
                SetPuyo(oldSubPos.Colum, oldSubPos.Row, Puyo.None);
                SetPuyo(newSubPos.Colum, newSubPos.Row, SubDropingPuyo);
            }
        }

        void PuyoMoveLeftIfCan()
        {
            int oldX = puyoPositionX;
            int oldY = puyoPositionY;

            Position subPosition = GetSubPuyoPosition(puyoPositionX, puyoPositionY, PuyoAngle);
            int oldSubX = subPosition.Colum;
            int oldSubY = subPosition.Row;

            // �ړ��ł���̂��H
            int newX = puyoPositionX - 1;
            int newY = puyoPositionY;
            int newSubX = oldSubX - 1;
            int newSubY = subPosition.Row;

            bool ret1 = CanMove(new Position(newX, newY));
            bool ret2 = CanMove(new Position(newSubX, newSubY));

            if(ret1 && ret2)
            {
                SetPuyo(oldX, oldY, Puyo.None);
                SetPuyo(oldSubX, oldSubY, Puyo.None);

                puyoPositionX = newX;
                puyoPositionY = newY;
                SetPuyo(puyoPositionX, puyoPositionY, DropingPuyo);
                SetPuyo(newSubX, newSubY, SubDropingPuyo);
            }
        }

        void PuyoMoveRightIfCan()
        {
            int oldX = puyoPositionX;
            int oldY = puyoPositionY;

            Position subPosition = GetSubPuyoPosition(puyoPositionX, puyoPositionY, PuyoAngle);
            int oldSubX = subPosition.Colum;
            int oldSubY = subPosition.Row;

            // �ړ��ł���̂��H
            int newX = puyoPositionX + 1;
            int newY = puyoPositionY;
            int newSubX = oldSubX + 1;
            int newSubY = subPosition.Row;

            bool ret1 = CanMove(new Position(newX, newY));
            bool ret2 = CanMove(new Position(newSubX, newSubY));

            if(ret1 && ret2)
            {
                SetPuyo(oldX, oldY, Puyo.None);
                if (oldSubY > -1)
                    SetPuyo(oldSubX, oldSubY, Puyo.None);

                puyoPositionX = newX;
                puyoPositionY = newY;
                SetPuyo(puyoPositionX, puyoPositionY, DropingPuyo);
                SetPuyo(newSubX, newSubY, SubDropingPuyo);
            }
        }
    }

}
