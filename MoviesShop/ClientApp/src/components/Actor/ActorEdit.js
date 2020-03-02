import React, { Component } from 'react';
import ActorList from './ActorList';
import ActorForm from './ActorForm';
import FindPanel from '../FindPanel';
import '../../app.css';

export class ActorEdit extends Component {
    constructor(props) {
        super(props);
        this.state = {
            id: 0,
            name: '',
            birthDay: '',
            countryDTO: '',
            //фильмы актёра
            filmsDTO: [],
            year: '',
            //веданные актёра
            forecasts: [],
            //индексы для поиска
            indexDelete: '',
            indexFinde: '',
            indexTitle: '',
            //флаг для редактирования
            flag: false,
            //модель фильма
            title: '',
            filmItem: '',
            //полный список фильмов
            fullListFilms: [],
            //новый набор фильмов
            newListFilms: [],


        };
        //изменение полей заполнения
        this.onChange = this.onChange.bind(this);
        this.onChangeArea = this.onChangeArea.bind(this);

        //методы поиска
        this.submitFind = this.submitFind.bind(this),
        this.submitFindeTitle = this.submitFindeTitle.bind(this),
        this.submitDelete = this.submitDelete.bind(this),

        //методы работы с данными
        this.submitEdit = this.submitEdit.bind(this),
        this.addFilm = this.addFilm.bind(this);

        //очистка формы 
        this.clearForm = this.clearForm.bind(this),


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


    onChange = e => {
        this.setState({ [e.target.name]: e.target.value })
    }

    onChangeArea = e => {
        this.setState({ [e.target.name]: e.target.value.split("\n") })
    }

    //////////////////////Методы для понели поиска
    submitFind = id => {
        fetch(`api/Actors?id=${encodeURIComponent(id)}`, {
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
        let answer = window.confirm('Are you sure about this?')
        if (answer) {
            fetch(`api/Actors?id=${encodeURIComponent(id)}`, {
                method: 'Delete',
            })
                .then(response => response.json())
                .then(data => {
                    this.setState({ forecasts: data, loading: false });
                });
        }
    }

    ///////////////////////Методы для формы редактирования

    submitEdit(forecast) {
        fetch('api/Film')
            .then(response => response.json())
            .then(data => {
                this.state.fullListFilms = data;
            });

        this.setState({
            id: forecast.id,
            name: forecast.name,
            birthDay: forecast.birthDay,
            countryDTO: forecast.countryDTO.countryTitle,
            filmsDTO: forecast.filmsDTO.map(item => item.title),
            flag: true,
        });
        console.log(this.state.fullListFilms)
        for (var i = 0; i < forecast.filmsDTO.length; i++) {
            for (var j = 0; j < this.state.fullListFilms.length; j++) {
                if (this.state.fullListFilms[j].title == forecast.filmsDTO[i].title) {
                    s: this.state.fullListFilms.splice(j, 1)
                    console.log(forecast.filmsDTO[i].title)
                }
            }
        };
    }

    addFilm(film) {

        const selectedData = this.state.fullListFilms.find(x => x.id == film);
        console.log(selectedData);
        this.setState({
            filmsDTO: [...this.state.filmsDTO, selectedData.title],
            newListFilms: [
                ...this.state.newListFilms,
                { filmId: selectedData.id }
            ]
        });
        this.deleteFilm(selectedData.id);
    }

    deleteFilm(film) {
        this.setState({
            fullListFilms: this.state.fullListFilms.filter(i => i.id !== film)
        });
    }

    clearForm() {
        this.setState({
            id: 0,
            name: '',
            birthDay: '',
            countryDTO: '',
            filmsDTO: [],
            flag: false,

        });
        fetch('api/Film')
            .then(response => response.json())
            .then(data => {
                this.setState({ fullListFilms: data });
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

                            id={this.state.id}
                            name={this.state.name}
                            birthDay={this.state.birthDay}
                            countryDTO={this.state.countryDTO}
                            flag={this.state.flag}
                            title={this.state.title}

                            filmId={this.state.filmId}
                            newListFilms={this.state.newListFilms}
                            filmItem={this.state.filmItem}

                            filmsDTO={this.state.filmsDTO}
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