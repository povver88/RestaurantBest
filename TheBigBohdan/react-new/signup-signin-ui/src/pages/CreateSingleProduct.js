import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
class CreateSingleProduct extends Component {

      constructor() {
        super();

        this.state = {
          products: []
        };


        this.BrClick = this.BrClick.bind(this);
        this.TakeAllSProducts = this.TakeAllSProducts.bind(this);
        
      }
      
      BrClick(e)
       {
         e.preventDefault();
         const config = {
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'Accept':'application/json'
          },
        };
        axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
        axios.defaults.headers.get['Accepts'] = 'application/json';
        axios.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
         axios.get('http://localhost:7071/api/CreateSingleProduct', config)
        .then(function(response){
          console.log(response.data);
        })
        .catch(function(error){
          console.log(error);
        });
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
        
      };
      
      
render() {
  const data = this.state.products;
const listItems = data.map((d) => <tr><td key={d.id}>{d.Name} </td> <td key={d.id}>{d.Amount} </td></tr> );

  return(
    <div className="FormCenter">
      <form  className="FormFields" >
        
        <div className="SomeButton">
          <button className='button' onClick={this.BrClick}>Read</button>
        </div>
        <div className="SomeButton">
          <button className='button' onClick={this.TakeAllSProducts}>Write</button>
        </div>
        
        <table>
        <tr><th>Name</th><th>Amount</th></tr>
         
            {listItems}
          
        </table>
        
        
    
      </form>

    </div>

  );

 }

}


export default CreateSingleProduct;
