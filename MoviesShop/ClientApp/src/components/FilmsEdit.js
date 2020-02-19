import React from 'react';
import '../app.css';
import FindPanel from './FindPanel';
import FilmsList from './FilmsList';
import FilmsForm  from './FilmsForm';

///////////////////////////////////////////////////////////////////
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

            indexDelete: '',
            indexFinde: '',
            indexTitle: '',

            flag: false,
            //<Item> - новый элемент который будет добавлен
            //<List> - список всех элементов которе находятся в БД
            genreTitle: '',
            genreItem: '',
            genreList: [],

            actorTitle: '',
            actorItem: '',
            actorList: [],
        };

            this.noChangeArea = this.onChangeArea.bind(this),
            this.noChange = this.onChange.bind(this),
            this.submitFind = this.submitFind.bind(this),
            this.submitFindeTitle = this.submitFindeTitle.bind(this),
            this.submitDelete = this.submitDelete.bind(this),
            this.submitEdit = this.submitEdit.bind(this),
            this.clearForm = this.clearForm.bind(this),

            this.addActor = this.addActor.bind(this),
            this.addGenre = this.addGenre.bind(this),
            
        fetch('api/Film')
                .then(response => response.json())
                .then(data => {
                    this.setState({ forecasts: data });
                });
        //Genre
        fetch('api/Genre')
            .then(response => response.json())
            .then(data => {
                this.setState({ genreList: data });
            });
        //Actor
        fetch('api/Actors')
            .then(response => response.json())
            .then(data => {
                this.setState({ actorList: data });
            });
    }

    //////////////////////Method for Panel
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

    ///////////////////////Method for Edit Form
    //submitNew = e => {
    //    e.preventDefault();
    //    alert("Film added"),
    //    fetch('api/Film', {
    //        method: 'post',
    //        headers: {
    //            'Content-Type': 'application/json'
    //        },
    //        body: JSON.stringify({
    //            title: this.state.title,
    //            year: this.state.year,
    //            genreDTO: this.state.genreDTO.map(function (item) { return { title: item } }),
    //            actorDTO: this.state.actorDTO.map(function (item) { return { name: item } }),
    //            countryDTO: {
    //                title: this.state.countryDTO
    //            },
    //        })
            
    //    })
    //        .then(() => {
    //            this.props.toggle();
    //        })
    //            .catch(err => console.log(err));


    //    this.setState({
    //        id: '',
    //        title: '',
    //        year: '',
    //        countryDTO: '',
    //        genreDTO: [],
    //        actorDTO: [],
    //    });
    //}

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

    clearForm()
    {
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

    onChange = e => {
        this.setState({ [e.target.name]: e.target.value })
    }

    onChangeArea = e => {
        console.log("values: ", e.target.value)
        this.setState({ [e.target.name]: e.target.value.split(",") })
    }

    addActor(actor) {
        this.setState({
            actorDTO:
                [...this.state.actorDTO, actor]
        });
    }

    addGenre(genre) {
        this.setState({
            genreDTO:
                [...this.state.genreDTO, genre]
        });
    }

    render() {
        return( <div className="content">
            <table>
                <tr>
                    <td>
                        <FilmsForm
                            forecasts={this.state.forecasts}

                            genreTitle={this.state.genreTitle}
                            genreItem={this.state.genreItem}
                            genreList={this.state.genreList}
                        
                            actorTitle={this.state.actorTitle}
                            actorItem={this.state.actorItem}
                            actorList={this.state.actorList}

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