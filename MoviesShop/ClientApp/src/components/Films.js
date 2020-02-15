import React, { Component } from 'react';
import '../app.css';
import GenreList from './GenreList';
import FindPanel from './FindPanel';

export class Films extends Component {

    constructor(props) {
        super(props);
        this.state = {
            indexDelete: '',
            indexFinde: '',
            indexTitle: '',
            forecasts: [],
            genres: [],
        };
            this.noChange = this.onChange.bind(this),
            this.submitFind = this.submitFind.bind(this),
            this.submitFindeTitle = this.submitFindeTitle.bind(this),
            this.submitDelete = this.submitDelete.bind(this),
            this.submitFindeGenre = this.submitFindeGenre.bind(this),

        fetch('api/Film')
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });

        fetch('api/Genre')
            .then(response => response.json())
            .then(data => {
                this.setState({ genres: data});
            });

    }
    
    submitFind = id => {
        fetch(`api/Film?Id=${encodeURIComponent(id)}`, {
            method: 'Get',
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });
    }

    submitFind = id => {
        fetch(`api/Film?Id=${encodeURIComponent(id)}`, {
            method: 'Get',
        })
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });
    }

    submitFindeTitle = title => {
        fetch(`api/Film/Title/${encodeURIComponent(title)}`, {
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
            fetch(`api/Film?Id=${encodeURIComponent(id)}`, {
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

    submitFindeGenre = genre =>
    {
        fetch(`api/Film/genre/${encodeURIComponent(genre)}`, {
            method: 'Get',
        })

        .then(response => response.json())
        .then(data => {
            this.setState({ forecasts: data, loading: false });
        });
    }


    render() {
        return (
            <div>
                <div className="genreposotion">
                    <GenreList
                        genres={this.state.genres}
                        submitFindeGenre={this.submitFindeGenre}
                    />
                </div>

                <div className="contentFilms">

                    <FindPanel
                        onChange={this.onChange}
                        submitFindeTitle={this.submitFindeTitle}
                        submitFind={this.submitFind}
                        submitDelete={this.submitDelete}
                        submitFindeGenre={this.submitFindeGenre}

                        indexDelete={this.state.indexDelete}
                        indexFinde={this.state.indexFinde}
                        indexTitle={this.state.indexTitle}
                    />

                    <div>
                        {this.state.forecasts.map(forecast =>
                            <div className="space">
                                <div className="contentbolck">
                                    <div class="im">
                                        <img src={forecast.urlImage} />
                                    </div>
                                    <div class="imageTit" />

                                    <div class="contentText">ID: {forecast.id} </div>
                                    <div class="contentText">Title: {forecast.title}</div>
                                    <div class="contentText">Year: {forecast.year}</div>
                                    <div class="contentText">Contru: {forecast.countryDTO.title}</div>
                                    <div class="contentText">Genre:&nbsp;
                                    {forecast.genreDTO.map(forc => <div class="contentList">{forc.title}.&nbsp; </div>)}
                                    </div>
                                    <div class="contentText">Actors:&nbsp;
                                    {forecast.actorDTO.map(act => <div class="contentList">{act.name}.&nbsp; </div>)}
                                    </div>
                                    <div className="box">
                                        <button class="editButton"  >
                                            Button
                                     </button>
                                    </div>
                                </div>
                            </div>
                        )}
                    </div>
                    <div class="AddDownToMainFilm" />
                </div>
               
            </div>
        );
    }
}
