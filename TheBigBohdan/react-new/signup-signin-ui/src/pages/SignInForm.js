import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
class SignInForm extends Component {


      state = {
        email: '',
        password: ''
      };


    handleChange(e) {
      let target = e.target;
      let value = target.value;
      let name = target.name;


    this.setState({
            email: value
    });
  }

    handleSubmit(e) {
      e.preventDefault();
      let data = {
        email: 'Bob',
        password: 'qwerty'
      };
      axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
      axios.defaults.headers.get['Accepts'] = 'application/json';
      axios.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
      axios.post("http://localhost:7071/api/LogIn", data ).then(response => console.log(response.json()));
      console.log('The form was submitted with the following data:');
      //console.log(this.data);
    };


    
    render() {

      return(
        <div className="FormCenter">
          <form onSubmit={this.handleSubmit} className="FormFields" onSubmit={this.handleSubmit}>
          <div className="FormField">
            <label className="FormField__Label" htmlFor="email">E-mail Address</label>
            <input type="email" id="email" className="FormField__Input" placeholder="Enter your email" name="email" value={this.state.email} onChange={this.handleChange} />
          </div>

            <div className="FormField">
              <label className="FormField__Label" htmlFor="password">Password</label>
              <input type="password" id="password" className="FormField__Input" placeholder="Enter your password" name="password" value={this.state.password} onChange={this.handleChange} />
            </div>

            <div className="FormField">
                <button className="FormField__Button mr-20">Sign In</button> <Link to="/"
                className="FormField__Link">Create an account</Link>
            </div>
          </form>
        </div>
      );
     }
    }

    export default SignInForm;
