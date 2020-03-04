import React  from 'react';
import '../app.css';

const GenreList = props =>
{
    return (
        <div >
            {props.genres.map(genre =>
                <div>
                    <button className="genreButton"
                        onClick={() => props.submitFindeGenre(genre.id)}>
                        {genre.title}
                    </button> <p />
                </div>
            )}
        </div>
        );
}
export default GenreList