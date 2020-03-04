import React, { Component } from 'react';
import '../app.css';
import FindPanel from './FindPanel';
import ActorList from './ActorList';
export class Actors extends Component {

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

            fetch('api/Actors')
                .then(response => response.json())
                .then(data => {
                    this.setState({ forecasts: data });
                });
    }

    onChange = e => {
        this.setState({ [e.target.name]: e.target.value })
    }

    submitFind = id => {
        fetch(`api/Actors?Id=${encodeURIComponent(id)}`, {
            method: 'Get',
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });
    }

    submitFindeTitle = name => {
        fetch(`api/Actors/Name/${encodeURIComponent(name)}`, {
            method: 'Get',
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });
    }

    submitDelete = id => {
        let answer = window.confirm('Are you sure about this ID?')
        if (answer) {
            fetch(`api/Actors?Id=${encodeURIComponent(id)}`, {
                method: 'Delete',
            })
                .then(response => response.json())
                .then(data => {
                    this.setState({ forecasts: data, loading: false });
                });
        }
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
                            <ActorList
                                forecasts={forecasts}
                            />
                        </td>
                    </tr>
                </table>
            </div>
        );
    }
}
