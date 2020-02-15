import React, { Component } from 'react';
import ActorList from './ActorList';
import ActorForm from './ActorForm';
import FindPanel from './FindPanel';
import '../app.css';

export class ActorEdit extends Component {
    constructor(props) {
        super(props);
        this.state = {
            id: 0,
            name: '',
            birthDay: '',
            countryDTO: '',
            films: [],
            forecasts: [],

            indexDelete: '',
            indexFinde: '',
            indexTitle: '',

            flag: false,

            title: '',
            filmItem: '',
            fullListFilms: [],
        };

            this.onChange = this.onChange.bind(this),
            this.submitFind = this.submitFind.bind(this),
            this.submitFindeTitle = this.submitFindeTitle.bind(this),
            this.submitDelete = this.submitDelete.bind(this),
            this.submitEdit = this.submitEdit.bind(this),
            this.clearForm = this.clearForm.bind(this),
            
            this.addFilm = this.addFilm.bind(this);

        fetch('api/Actors')
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data });
            });
        
        fetch('api/Film')
            .then(response => response.json())
            .then(data => {
                this.setState({ fullListFilms: data });
            });

    }

    //////////////////////Method for Panel
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

    ///////////////////////Method for Edit Form
    submitEdit(forecast) {
        this.setState({
            id: forecast.id,
            name: forecast.name,
            birthDay: forecast.birthDay,
            countryDTO: forecast.countryDTO.title,
            films: forecast.films.map(item => item.title),
            flag: true,
        })
    }

    clearForm() {
        this.setState({
            id: 0,
            name: '',
            birthDay: '',
            countryDTO: '',
            films: [],
            flag: false,
        })
    }

    onChange = e => {
        this.setState({ [e.target.name]: e.target.value })
    }

    onChangeArea = e => {
        this.setState({ [e.target.name]: e.target.value.split("\n") })
    }

    addFilm(film) {
        this.setState({
            films:
                [...this.state.films, film]
        });
    }

    render() {
        return (<div className="content">
                <table>
                    <tr>
                        <td>
                            <ActorForm 
                                forecasts={this.state.forecasts}
                                fullListFilms={this.state.fullListFilms}
                                filmItem={this.state.filmItem}
                                title={this.state.title}

                                id={this.state.id}
                                name={this.state.name}
                                birthDay={this.state.birthDay}
                                countryDTO={this.state.countryDTO}
                                films={this.state.films}
                                flag={this.state.flag}

                                addFilm={this.addFilm}

                                clearForm={this.clearForm}
                                onChange={this.onChange}
                                onChangeArea={this.onChangeArea}
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

                            <ActorList
                                forecasts={this.state.forecasts}
                                submitDelete={this.submitDelete}
                                submitEdit={this.submitEdit}
                            />
                        </td>
                    </tr>
                </table>
                <div class="AddDownToMain" />
            </div>
        );
    }
}
export default ActorEdit;