using HomeFinanceApp.Controllers;
using HomeFinanceApp.Core.Enums;
using HomeFinanceApp.Core.Interfaces;
using HomeFinanceApp.Services;
using HomeFinanceApp.Views.Forms;
using HomeFinanceApp.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeFinanceApp.Views
{
    internal partial class FinanceForm : Form, IFinanceView
    {
        #region Fields and Properties
        public FinanceController financeController;

        private Dictionary<int, PictureBox> _membersPictures = new();
        private Dictionary<int, int> _idToRole = new();
        private Point sizeOfMoney = new Point(72, 34);
        private Image moneyImage = ConvertHelper.ByteArrayToImage(Properties.Resources.money);

        private List<RoleVisual> _roleVisuals = new()
        {
            new RoleVisual(0, new Point(424, -215), new Point(424, -1), new Point(436, 99), Properties.Resources.son),
            new RoleVisual(1, new Point(381, 518), new Point(381, 249), new Point(524, 260), Properties.Resources.mother),
            new RoleVisual(2, new Point(967, 109), new Point(784, 109), new Point(706, 191), Properties.Resources.daughter),
            new RoleVisual(3, new Point(-4, 175), new Point(-4, 112), new Point(149, 194), Properties.Resources.father),
        };
        #endregion

        public FinanceForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            financeController.Start();
        }

        #region Animation Methods
        private async Task MovePictureBox(PictureBox pictureBox, Point targetPosition, int durationMs = 100)
        {
            Console.WriteLine($"Moving from {pictureBox.Location} to {targetPosition}");
            Point startPosition = pictureBox.Location;
            float distanceX = targetPosition.X - startPosition.X;
            float distanceY = targetPosition.Y - startPosition.Y;
            int steps = Math.Max(1, durationMs / 16);

            float stepX = distanceX / steps;
            float stepY = distanceY / steps;

            for (int i = 1; i <= steps; i++)
            {
                int newX = startPosition.X + (int)(stepX * i);
                int newY = startPosition.Y + (int)(stepY * i);

                this.InvokeIfRequired(() =>
                {
                    pictureBox.Left = newX;
                    pictureBox.Top = newY;
                });

                await Task.Delay(16);
            }

            this.InvokeIfRequired(() =>
            {
                pictureBox.Left = targetPosition.X;
                pictureBox.Top = targetPosition.Y;
            });
        }
        #endregion

        #region PictureBox Creation Methods
        private PictureBox CreateMemberDependOnRole(int roleId)
        {
            var member = new PictureBox();
            member.SizeMode = PictureBoxSizeMode.StretchImage;
            member.Size = new Size(137, 219);
            member.BackColor = Color.Transparent;

            member.Image = _roleVisuals.First(x => x.roleId == roleId).image;
            member.Location = _roleVisuals.First(x => x.roleId == roleId).non_visible_position;

            this.InvokeIfRequired(() => Controls.Add(member));
            return member;
        }

        private PictureBox CreateMoneyPicture(Point location)
        {
            var picture = new PictureBox();
            picture.Image = moneyImage;
            picture.Location = location;
            picture.Size = new Size(sizeOfMoney.X, sizeOfMoney.Y);

            this.InvokeIfRequired(() => Controls.Add(picture));
            return picture;
        }
        #endregion

        #region IFinanceView Implementation
        public void CreateMember(int memberId, int roleId)
        {
            _idToRole[memberId] = roleId;
            _membersPictures[memberId] = CreateMemberDependOnRole(roleId);
            if (roleId == 2)
            {
                Console.WriteLine("zxc");
            }
        }

        public async void FamilyPostponedMoney(decimal money)
        {
            var temp = CreateMoneyPicture(moneysPicture.Location);

            await MovePictureBox(temp, savingsPicture.Location);
            this.InvokeIfRequired(() => Controls.Remove(temp));
        }

        public async void GatherEnded()
        {
            var moveTasks = new List<Task>();
            foreach (var member in _membersPictures)
                moveTasks.Add(MovePictureBox(member.Value, _roleVisuals.First(x => x.roleId == _idToRole[member.Key]).non_visible_position));

            await Task.WhenAll(moveTasks);
        }

        public void MemberDropMoney(int memberId, decimal money)
        {
            this.InvokeIfRequired(() =>
            {
                var tempMoney = CreateMoneyPicture(_roleVisuals.First(x => x.roleId == _idToRole[memberId]).position_of_money);
            });
        }

        public async void MemberGetMoney(int memberId, decimal money)
        {
            var temp = CreateMoneyPicture(moneysPicture.Location);

            await MovePictureBox(temp, _roleVisuals.First(x => x.roleId == _idToRole[memberId]).position_of_money);
        }

        public async void MemberInputMoneyToSavings(int memberId, decimal money)
        {
            var temp = CreateMoneyPicture(_roleVisuals.First(x => x.roleId == _idToRole[memberId]).position_of_money);
            await MovePictureBox(temp, savingsPicture.Location);
            this.InvokeIfRequired(() => Controls.Remove(temp));
        }

        public async void MemberNeedExtraMoneyFromMoneyBox(int memberId, decimal money)
        {
            var temp = CreateMoneyPicture(savingsPicture.Location);

            await MovePictureBox(temp, _roleVisuals.First(x => x.roleId == _idToRole[memberId]).position_of_money);
            this.InvokeIfRequired(() => Controls.Remove(temp));
        }

        public async void StartNewMonth()
        {
            var moveTasks = new List<Task>();
            foreach (var member in _membersPictures)
            {
                var task = Task.Run(() => MovePictureBox(member.Value, _roleVisuals.First(x => x.roleId == _idToRole[member.Key]).position_near_to_table));
                moveTasks.Add(task);
            }
            await Task.WhenAll(moveTasks);
        }

        public void SavingsChanged(decimal money)
        {
            this.InvokeIfRequired(() => savingsLabel.Text = money.ToString());
        }

        public void AmountChanged(decimal money)
        {
            this.InvokeIfRequired(() =>
            {
                bool shouldShow = money != 0;
                moneysLabel.Visible = shouldShow;
                moneysPicture.Visible = shouldShow;
                moneysLabel.Text = money.ToString();
            });
        }


        #endregion

        #region IObserver Implementation
        void IObserver.OnFamilyEvent(FamilyEvents eventType, int memberId, decimal money)
        {
            // Все обработчики событий уже вызываются через InvokeIfRequired в своих методах
            switch (eventType)
            {
                case FamilyEvents.CreateMember:
                    CreateMember(memberId, (int)money);
                    break;
                case FamilyEvents.AmountValueChanged:
                    AmountChanged(money);
                    break;
                case FamilyEvents.SavingsValueChanged:
                    SavingsChanged(money);
                    break;
                case FamilyEvents.MemberGetMoney:
                    MemberGetMoney(memberId, money);
                    break;
                case FamilyEvents.MemberDropMoney:
                    MemberDropMoney(memberId, money);
                    break;
                case FamilyEvents.MemberInputMoneyToSavings:
                    MemberInputMoneyToSavings(memberId, money);
                    break;
                case FamilyEvents.MemberNeedExtraMoneyFromMoneyBox:
                    MemberNeedExtraMoneyFromMoneyBox(memberId, money);
                    break;
                case FamilyEvents.FamilyPostponedMoney:
                    FamilyPostponedMoney(money);
                    break;
                case FamilyEvents.StartNewMonth:
                    StartNewMonth();
                    break;
                case FamilyEvents.GatherEnded:
                    GatherEnded();
                    break;
            }
        }
        #endregion
    }
}