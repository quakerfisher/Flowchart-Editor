﻿using FlowchartEditorMVP.Model;
using FlowchartEditorMVP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowchartEditorMVP.Presenter
{
    interface IFlowchartPresenter : IPresenter
    {
        void AddBlock(string str, int x, int y, int scroll);
        void EditBlock(string str, int x, int y, int scroll);
        void RemoveBlock(int x, int y, int scroll);
        bool IsEdge(int xCoordsClick, int yCoordsClick, int scroll);
        bool IsSquareBlock(int xCoordsClick, int yCoordsClick, int scroll);
        void ToCode();
        void Apply(string name, string owner);
        void Decline();
        List<Tuple<string, string>> GetReviewsAndLogins();
        void LoadReviewedFlowchart(string reviewer, string name);
        void ToDataBase();
        string GetLogin();
        void ToChooseFlowchart();
        IFlowchart getFlowchart();
        void FlowchartMouseClick(int x, int y, int scroll);
    }

    class MasterPresenter : IFlowchartPresenter
    {
        private IFlowchart flowchart;
        private DataManagement data;
        //private CodeFactory code;
        private MasterView view;


        public MasterPresenter(DataManagement data, string path, MasterView view, string name, string type_code)
        {
            switch (type_code)
            {
                case "C++":
                {
                    flowchart = new FlowchartCppFactory().CreateFlowchart(path, name);
                    break;
                }
                default:
                {
                        flowchart = new Flowchart(name);
                        break;
                } 
            }
            this.data = data;
            this.view = view;
        }

        public void ToChooseFlowchart()
        {
            ChooseFlowchartView chooseFlowchartView = new ChooseFlowchartView(data);
            view.Hide();
            chooseFlowchartView.Show();
        }

        public MasterPresenter(DataManagement data, MasterView view, string name)
        {                   
            this.data = data;
            this.view = view;
            flowchart = data.LoadFlowchart(name);
        }

        public string GetLogin()
        {
            return data.GetLogin();
        }

        public void Apply(string name, string owner) { }
        public void Decline()
        {
        }
        public void ToCode()
        {
            ICode code = new CppFactory().
                CreateCode(flowchart);
            code.WriteFile(@"MyTest.cpp");
        }
        public bool IsEdge(int xCoordsClick, int yCoordsClick, int scroll)
        {
            return true;
        }
        public bool IsSquareBlock(int xCoordsClick, int yCoordsClick, int scroll)
        {
            return flowchart.GetBlock(xCoordsClick, yCoordsClick, scroll).IsSquare();
        }
        public void Run() { }
        public void AddBlock(string str, int x, int y, int scroll)
        {
            
        }
        public void EditBlock(string str, int x, int y, int scroll)
        {
            if (IsSquareBlock(x, y, scroll))
            {
                IBlock block = flowchart.GetBlock(x, y, scroll);
                flowchart.AddStrToBlock(block, str);
            }
        }
        public void RemoveBlock(int x, int y, int scroll)
        {
            if (IsSquareBlock(x, y, scroll))
            {
                IBlock block = flowchart.GetBlock(x, y, scroll);
                flowchart.DeleteSquareBlock((SquareBlock)block);
            }                
        }
        public List<Tuple<string, string>> GetReviewsAndLogins()
        {
            return new List<Tuple<string, string>>();
        }
        public void LoadReviewedFlowchart(string reviewer,string name)
        {
            flowchart = data.LoadFlowchart(name);
        }
        public void ToDataBase()
        {
            data.AddToDB(flowchart);
        }
        public IFlowchart getFlowchart()
        {
            return flowchart;
        }

        public void FlowchartMouseClick(int x, int y, int scroll)
        {
            IBlock block = flowchart.GetBlock(x, y, scroll);
            view.ShowBlockContent(block);
        }
    }

    class ReviewerPresenter : IFlowchartPresenter
    {
        private IFlowchart flowchart;
        private DataManagement data;        
        private ReviewerView view;

        public void FlowchartMouseClick(int x, int y, int scroll)
        {
            //IBlock block = flowchart.GetBlock(x, y);
            //view.ShowBlockContent(block);
        }

        public string GetLogin()
        {
            return data.GetLogin();
        }

        public void ToChooseFlowchart()
        {
            ChooseFlowchartView chooseFlowchartView = new ChooseFlowchartView(data);
            view.Hide();
            chooseFlowchartView.Show();
        }

        public ReviewerPresenter(DataManagement data, ReviewerView view, string name)
        {
        }
        public void Apply(string name, string owner)
        {
            flowchart = data.LoadFlowchart(name);
            this.data = data;
        }
        public void Decline()
        { }
        public void ToCode()
        {
            ICode code = new CppFactory().
                CreateCode(flowchart);
            code.WriteFile(@"MyTest.cpp");
        }
        public bool IsEdge(int xCoordsClick, int yCoordsClick, int scroll)
        {
            return true;
        }
        public bool IsSquareBlock(int xCoordsClick, int yCoordsClick, int scroll)
        {
            return true;
        }
        public void Run() { }
        public void AddBlock(string str, int x, int y, int scroll)
        {
            flowchart.AddBlock(new SquareBlock(), new Edge());
        }
        public void EditBlock(string str, int x, int y, int scroll)
        {
            flowchart.AddStrToBlock(new SquareBlock(), str);
        }
        public void RemoveBlock(int x, int y, int scroll)
        {
            flowchart.DeleteSquareBlock(new SquareBlock());
        }
        public List<Tuple<string, string>> GetReviewsAndLogins()
        {
            return new List<Tuple<string, string>>();
        }
        public void LoadReviewedFlowchart(string reviewer, string name)
        {
            flowchart = data.LoadFlowchart(name);
        }
        public void ToDataBase()
        {
            //data.AddToDB(flowchart);
            data.AddToDB(flowchart);
        }
        public IFlowchart getFlowchart()
        {
            return flowchart;
        }
    }
}
