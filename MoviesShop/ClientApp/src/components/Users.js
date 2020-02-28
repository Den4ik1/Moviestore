import React, { Component } from 'react';
import '../app.css';
import FindPanel from './FindPanel';
import UserList from './UserList';

export class Users extends Component {

    constructor(props) {
        super(props);
        this.state = {
            indexDelete: '',
            indexFinde: '',
            indexTitle: '',
            forecasts: [], loading: true
        };
            this.noChange = this.onChange.bind(this),
            this.submitFind = this.submitFind.bind(this),
            this.submitFindeTitle = this.submitFindeTitle.bind(this),
            this.submitDelete = this.submitDelete.bind(this),

        fetch('api/Users', {
            method: 'get'})
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });
    }

    submitFind = id => {
        fetch(`api/Users?id=${encodeURIComponent(id)}`, {
            method: 'Get',
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });
    }

    submitFindeTitle = name => {
        fetch(`api/Users/Name/${encodeURIComponent(name)}`, {
            method: 'Get',
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });
    }

    submitDelete = id => {
        let answer = window.confirm('Are you sure about this?')
        if (answer) {
            fetch(`api/Users?id=${encodeURIComponent(id)}`, {
                method: 'Delete',
            })
                .then(response => response.json())
                .then(data => {
                    this.setState({ forecasts: data, loading: false });
                });
        }
    }

    onChange = e => {
        this.setState({ [e.target.name]: e.target.value })
    }
    

    render() {
        const { forecasts } = this.state;
        return (
            <div className="content">
                <table>
                    <tr>
                        <td>
                            <FindPanel
                                onChange={this.onChange}
                                submitFindeTitle={this.submitFindeTitle}
                                submitFind={this.submitFind}
                                submitDelete={this.submitDelete}

                                indexDelete={this.state.indexDelete}
                                indexFinde={this.state.indexFinde}
                                indexTitle={this.state.indexTitle}
                            />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <UserList
                                forecasts={forecasts}
                            />
                        </td>
                    </tr>
                </table>
                <div class="AddDownToMain" />
            </div>
        );
    }
}

