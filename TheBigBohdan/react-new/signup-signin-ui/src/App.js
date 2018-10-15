import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Link, NavLink } from 'react-router-dom';
import SignUpForm from './pages/SignUpForm';
import SignInForm from './pages/SignInForm';
import CreateSingleProduct from './pages/CreateSingleProduct';
import CreateProduct from './pages/CreateProduct';
import './App.css';

class App extends Component {
  render() {
    return (
      <Router>
      <div className="App">
        <div className="App__Aside"></div>
        <div className="App__Form">
          <div className="PageSwitcher">
            <NavLink to="/create-single-product" activeClassName="PageSwitcher__Item--Active" className="PageSwitcher__Item">Create</NavLink>
            <NavLink to="/create-product" activeClassName="PageSwitcher__Item--Active" className="PageSwitcher__Item">CreateProduct</NavLink>
            <NavLink to="/sign-in" activeClassName="PageSwitcher__Item--Active" className="PageSwitcher__Item">Sign In</NavLink> 
            <NavLink exact to="/" activeClassName="PageSwitcher__Item--Active" className="PageSwitcher__Item">Sign Up</NavLink>
          </div>


          <div className="FormTitle">
              <NavLink to="/sign-in" activeClassName="FormTitle__Link--Active" className="FormTitle__Link">Sign In</NavLink> or <NavLink exact to="/" activeClassName="FormTitle__Link--Active" className="FormTitle__Link">Sign Up</NavLink> or <NavLink exact to="/create-single-product" activeClassName="FormTitle__Link--Active" className="FormTitle__Link">Create</NavLink> or <NavLink exact to="/create-product" activeClassName="FormTitle__Link--Active" className="FormTitle__Link">CreateProduct</NavLink>
          </div>
          <Route exact path="/" component={SignUpForm}>
          </Route>
            <Route path="/sign-in" component={SignInForm}>
              <h1>Sign In</h1>   
          </Route>
          <Route path="/create-single-product" component={CreateSingleProduct}>
              <h1>Create</h1>   
          </Route>
          <Route path="/create-product" component={CreateProduct}>
              <h1>CreateProduct</h1>   
          </Route>
        </div>

      </div>
      </Router>
    );
  }
}

export default App;
