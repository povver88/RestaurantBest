import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
class CreateProduct extends Component {

      constructor() {
        super();

        this.state = {
          products: [],
          Products: [],
          Amounts: [],
          Name: '',
          Price: '',
          Time: ''
          
        };
        
        this.TakeAllSProducts = this.TakeAllSProducts.bind(this);
        this.handleChangeChk = this.handleChangeChk.bind(this);
        this.handleChangeRange = this.handleChangeRange.bind(this);
        this.handleChangeName = this.handleChangeName.bind(this);
        this.handleChangePrice = this.handleChangePrice.bind(this);
        this.CreateNewProduct = this.CreateNewProduct.bind(this);
        this.handleChangeTime = this.handleChangeTime.bind(this);
        
      }
      
      handleChangeChk(e, id)
      {
          let sm = id;
          this.state.Products.push(sm);
      }
      handleChangeRange(e)
      {
        if(e.key === "Enter")
        {
          let st = e.target.value;
        this.state.Amounts.push(st);
        }
        
        
      }
      handleChangeName(e)
      {
        let st = e.target.value;
        this.setState({Name: st});
        
      }
      handleChangePrice(e)
      {
        let st = e.target.value;
        this.setState({Price: st});
        
      }
      handleChangeTime(e)
      {
        let st = e.target.value;
        this.setState({Time: st});
        
      }
      
      TakeAllSProducts (e) 
      {
        e.preventDefault()
        let products = [];
        const config = {
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'Accept':'application/json'
          },
        };
        axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
        axios.defaults.headers.get['Accepts'] = 'application/json';
        axios.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        axios.get('http://localhost:7071/api/TakeSingleProducts', config)
        .then((getResponse) => {
          console.log(JSON.parse(getResponse.data));
          products = JSON.parse(getResponse.data);
          
          this.setState({products});
          
        })
        
      }
      CreateNewProduct()
      {
        const data = {Name:this.state.Name, Price:this.state.Price, Time:this.state.Time, Products:this.state.Products, Amounts:this.state.Amounts}
        const config = {
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'Accept':'application/json'
          },
        };
        axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
        axios.defaults.headers.get['Accepts'] = 'application/json';
        axios.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        axios.post("http://localhost:7071/api/CreateProduct", data)
        .then((getResponse) => {
          console.log(getResponse);
        })
        .catch(function(error){
          console.log(error);
        })
        
      };
      
      
render() {
  const data = this.state.products;
const listItems = data.map((d) => <tr><td key={d.id}><input type="checkbox" onChange={(e) => this.handleChangeChk(e, d.Name)} />{d.Name} </td> <td key={d.id}>{d.Amount}</td><input type="text" onKeyDown={this.handleChangeRange} /></tr> );
  return(
      
    <div className="FormCenter">
    
      <form  className="FormFields" >
        
        
        <div className="SomeButton">
          <button className='button' onClick={this.TakeAllSProducts}>Write</button>
        </div>
        <div className="SomeButton">
          <input type="text" onChange={this.handleChangeName} placeholder="Name"></input>
        </div>
        <div className="SomeButton">
          <input type="text" onChange={this.handleChangePrice} placeholder="Price"></input>
        </div>
        <div className="SomeButton">
          <input type="text" onChange={this.handleChangeTime} placeholder="Time"></input>
        </div>
        <table>
        <tr><th>Name</th><th>Amount</th></tr>
         
            {listItems}
          
        </table>
        <div>
          <button onClick={this.CreateNewProduct}>Succes</button>
        </div>
        
    
      </form>

    </div>

  );

 }

}


export default CreateProduct;
