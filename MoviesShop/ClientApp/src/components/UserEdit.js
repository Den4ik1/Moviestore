import React, { Component } from 'react';
import '../app.css';
import FindPanel from './FindPanel';
import UserList from './UserList';
import UserForm from './UserForm';

///////////////////////////////////////////////////////////////////
export class UserEdit extends Component {
    constructor(props) {
        super(props);
        this.state = {
            id: 0,
            name: '',
            age: '',
            title: '',
            films: '',
            indexDelete: '',
            indexFinde: '',
            indexTitle: '',
            forecasts: [],

            flag: false,
        };
            this.onChange = this.onChange.bind(this),
            this.submitFind = this.submitFind.bind(this),
            this.submitFindeTitle = this.submitFindeTitle.bind(this),
            this.submitDelete = this.submitDelete.bind(this),
            this.submitEdit = this.submitEdit.bind(this);
            this.clearForm = this.clearForm.bind(this);

            fetch('api/Users', {
                        method: 'get'
                })
                .then(response => response.json())
                .then(data => {
                    this.setState({ forecasts: data});
                });
    }

    //////////////////////Методы для понели поиска
    submitFind = id => {
        fetch(`api/Users?id=${encodeURIComponent(id)}`, {
            method: 'Get',
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data});
            });
    }

    submitFindeTitle = name => {
        fetch(`api/Users/Name/${encodeURIComponent(name)}`, {
            method: 'Get',
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data});
            });
    }

    submitDelete = id => {
        let answer = window.confirm('Are you sure about this?')
        if (answer) {
            fetch(`api/Users?Id=${encodeURIComponent(id)}`, {
                method: 'Delete',
            })
                .then(response => response.json())
                .then(data => {
                    this.setState({ forecasts: data});
                });
        }
    }

    ///////////////////////Методы дял EditForm

    submitEdit(forecast) {
        this.setState({
            id: forecast.id,
            name: forecast.name,
            age: forecast.age,

            flag: true,
        })
    }

    clearForm() {
        this.setState({
            id: 0,
            name: '',
            age: '',

            flag: false,
        })
    }

    onChange = e => {
        this.setState({ [e.target.name]: e.target.value })
    }


    render() {
        return (<div className="content">
            <table>
                <tr>
                    <td>
                        <UserForm
                            forecasts={this.state.forecasts}
                            id={this.state.id}
                            name={this.state.name}
                            age={this.state.age}
                            flag={this.state.flag}

                            clearForm={this.clearForm}
                            onChange={this.onChange}
                            submitEdit={this.submitEdit}
                        />
                    
                        <FindPanel
                            onChange={this.onChange}
                            submitFindeTitle={this.submitFindeTitle}
                            submitFind={this.submitFind}
                            submitDelete={this.submitDelete}

                            indexDelete={this.state.indexDelete}
                            indexFinde={this.state.indexFinde}
                            indexTitle={this.state.indexTitle}
                        />
                   
                        <UserList
                            forecasts={this.state.forecasts}
                            submitEdit={this.submitEdit}
                            submitDelete={this.submitDelete}
                        />
                    </td>
                </tr>
            </table>
            <div class="AddDownToMain" />
        </div>);
    }
}

export default UserEdit;