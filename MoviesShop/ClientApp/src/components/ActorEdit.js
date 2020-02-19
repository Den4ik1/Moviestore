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
            //фильмы актёра
            filmsDTO: [],
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
            filmItem:'',
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

    ///////////////////////Методы лоя вырмы редактирования
    submitEdit(forecast) {
        this.setState({
            id: forecast.id,
            name: forecast.name,
            birthDay: forecast.birthDay,
            countryDTO: forecast.countryDTO.title,
            filmsDTO: forecast.filmsDTO.map(item => item.title),
            flag: true,
        })
    }

    addFilm(film) {

        const selectedData = this.state.fullListFilms.find(x => x.id == film);
        console.log(selectedData);
        this.setState({
            filmsDTO: [...this.state.filmsDTO, selectedData.title],
            newListFilms: [
                ...this.state.newListFilms,
                { filmId: selectedData.id}
            ]
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
        })
    }

    render() {
        return (<div className="content">
                <table>
                    <tr>
                        <td>
                            <ActorForm 
                                forecasts={this.state.forecasts}
                                fullListFilms={this.state.fullListFilms}

                                
                                filmId={this.state.filmId}

                                id={this.state.id}
                                name={this.state.name}
                                birthDay={this.state.birthDay}
                                countryDTO={this.state.countryDTO}
                                flag={this.state.flag}
                                title={this.state.title}

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