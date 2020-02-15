import React, { Component } from 'react';
import { Route } from 'react-router';
import { UserEdit } from './components/UserEdit';
import Header from './components/Header';
import Main from './components/Main';
import './app.css'

class App extends Component
{
    render() {
        return (
            <div className="main" >
                <Route exact path='/userEdit' Component={UserEdit} />
                <Header />
                <Main />
            </div>
        );
    }
}
export { App }