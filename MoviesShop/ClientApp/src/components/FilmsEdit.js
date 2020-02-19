import React from 'react';
import '../app.css';
import FindPanel from './FindPanel';
import FilmsList from './FilmsList';
import FilmsForm from './FilmsForm';

export class FilmsEdit extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            id: 0,
            title: '',
            year: '',
            countryDTO: '',
            genreDTO: [],
            actorDTO: [],
            urlImage: '',
            forecasts: [],
            //индексы для поиска
            indexDelete: '',
            indexFinde: '',
            indexTitle: '',
            //флаг для редактирования
            flag: false,

            ////////////////////////////////////
            //модель жанра
            genreItem: '',
            //полный список жанраов
            fullListGenre: [],
            //новый набор жанраов
            newListGenre: [],
            
            ////////////////////////////////////
            //модель актёра
            actorItem: '',
            //полный список актёров
            fullListActor: [],
            //новый набор актёров
            newListActor: [],
            
        };
            //изменение полей заполнения
            this.noChangeArea = this.onChangeArea.bind(this),
            this.noChange = this.onChange.bind(this),

            //методы поиска
            this.submitFind = this.submitFind.bind(this),
            this.submitFindeTitle = this.submitFindeTitle.bind(this),
            this.submitDelete = this.submitDelete.bind(this),

            //методы работы с данными
            this.submitEdit = this.submitEdit.bind(this),
            this.addActor = this.addActor.bind(this),
            this.addGenre = this.addGenre.bind(this),

             //очистка формы 
             this.clearForm = this.clearForm.bind(this),

        fetch('api/Film')
                .then(response => response.json())
                .then(data => {
                    this.setState({ forecasts: data });
                });
        //Genre
        fetch('api/Genre')
            .then(response => response.json())
            .then(data => {
                this.setState({ fullListGenre: data });
            });
        //Actor
        fetch('api/Actors')
            .then(response => response.json())
            .then(data => {
                this.setState({ fullListActor: data });
            });
    }

    onChange = e => {
        this.setState({ [e.target.name]: e.target.value })
    }

    onChangeArea = e => {
        console.log("values: ", e.target.value)
        this.setState({ [e.target.name]: e.target.value.split(",") })
    }

    //////////////////////Методы для понели поиска
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

    ///////////////////////Методы для формы редактирования

    submitEdit(forecast) {
        this.setState({
            id: forecast.id,
            title: forecast.title,
            year: forecast.year,
            countryDTO: forecast.countryDTO.title,
            genreDTO: forecast.genreDTO.map(item => item.title),
            actorDTO: forecast.actorDTO.map(item => item.title),
            flag: true,
        })
    }
    
    addActor(actor) {
        const selectedData = this.state.fullListActor.find(x => x.id == actor);
        //console.log(selectedData);
        this.setState({
            actorDTO: [...this.state.actorDTO, selectedData.name],
            newListActor: [
                ...this.state.newListActor,
                { actorId: selectedData.id }
            ]
        });
    }

    addGenre(genre) {
        const selectedData = this.state.fullListGenre.find(x => x.id == genre);
        console.log(selectedData);
        this.setState({
            genreDTO: [...this.state.genreDTO, selectedData.title],
            newListGenre: [
                ...this.state.newListGenre,
                { genreId: selectedData.id }
            ]
        });
    }

    clearForm() {
        this.setState({
            id: 0,
            title: '',
            year: '',
            countryDTO: '',
            genreDTO: [],
            actorDTO: [],
            urlImage: '',
            flag: false,
        })
    }

    render() {
        return (<div className="content">
            <table>
                <tr>
                    <td>
                        <FilmsForm
                            forecasts={this.state.forecasts}
                            fullListActor={this.state.fullListActor}
                            fullListGenre={this.state.fullListGenre}

                            genreId={this.state.genreId}
                            genreItem={this.state.genreItem}
                            newListGenre={this.state.newListGenre}

                            actorId={this.state.actorId}
                            actorItem={this.state.actorItem}
                            newListActor={this.state.newListActor}

                            id={this.state.id}
                            title={this.state.title}
                            year={this.state.year}
                            countryDTO={this.state.countryDTO}
                            genreDTO={this.state.genreDTO}
                            actorDTO={this.state.actorDTO}
                            urlImage={this.state.urlImage}
                            flag={this.state.flag}

                            addActor={this.addActor}
                            addGenre={this.addGenre}

                            clearForm={this.clearForm}
                            onChange={this.onChange}
                            onChangeArea={this.onChangeArea}
                        />
                    </td>
                </tr>
                <tr>
                    <td>
                        <FindPanel
                            onChangeArea={this.onChangeArea}
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
                        <FilmsList
                            forecasts={this.state.forecasts}
                            submitEdit={this.submitEdit}
                            submitDelete={this.submitDelete}
                        />
                    </td>
                </tr>
            </table >
            <div class="AddDownToMain" />
        </div>);
    }
}

export default FilmsEdit;