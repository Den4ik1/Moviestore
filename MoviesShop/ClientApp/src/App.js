import React, { Component } from 'react';
import { Route } from 'react-router';
import { Films } from './components/Film/Films';
import Header from './components/Header';
import Main from './components/Main';
import './app.css'

class App extends Component
{
    render() {
        return (
            <div className="main" >
                <Route exact path='/Film' Component={Films} />
                <Header />
                <Main />
            </div>
        );
    }
}
export { App }