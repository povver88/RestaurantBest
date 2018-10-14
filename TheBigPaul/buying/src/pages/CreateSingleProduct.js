import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

class CreateSingleProduct extends Component {

      constructor() {
        super();

        this.state = {
          products: [{Name: " ",
           Amount: " "}]
        };


        this.OnClick = this.OnClick.bind(this);
        this.TakeAllSProducts = this.TakeAllSProducts.bind(this);
      }
      OnClick()
      {
        axios.get('http://localhost:7071/api/CreateSingleProduct')
        .then(function(response){
          console.log(response);
          this.setState({products: response.data})
        })
        .catch(function(error){
          console.log(error);
        });
      }
      TakeAllSProducts()
      {
        axios.get('http://localhost:7071/api/TakeSingleProducts')
        .then(function(response){
          this.state.products = response.products;
          console.log(this.state.products)

        })
        .catch(function(error){
          console.log(error);
        });
      }
      
render() {

  return(
    <div className="FormCenter">
      <form className="FormFields">
        
        <div className="SomeButton">
          <button className='button' onClick={this.OnClick}>Read</button>
        </div>
        <div className="SomeButton">
          <button className='button' onClick={this.TakeAllSProducts}>Write</button>
        </div>
        
      </form>

    </div>

  );

 }

}


export default CreateSingleProduct;
