var gulp = require('gulp'),
    concat = require('gulp-concat'),
    uglify = require('gulp-uglify'),
    rename = require('gulp-rename'),
    sass = require('gulp-ruby-sass'),
    autoprefixer = require('gulp-autoprefixer'),
    browserSync = require('browser-sync').create();
    cache = require('gulp-cached'); //If cached version identical to current file then it doesn't pass it downstream so this file won't be copied 

gulp.task('default', 'copy-node_modules');

gulp.task('copy-node_modules', function () {

    try {

        gulp.src('node_modules/**')
            .pipe(cache('node_modules'))
            .pipe(gulp.dest('wwwroot/node_modules'));
    }
    catch (e) {
        return -1;
    }
    return 0;
});

var DEST = 'build/';

gulp.task('scripts', function () {
    return gulp.src([
        'src/js/helpers/*.js',
        'src/js/*.js',
    ])
        .pipe(concat('custom.js'))
        .pipe(gulp.dest(DEST + '/js'))
        .pipe(rename({ suffix: '.min' }))
        .pipe(uglify())
        .pipe(gulp.dest(DEST + '/js'))
        .pipe(browserSync.stream());
});

// TODO: Maybe we can simplify how sass compile the minify and unminify version
var compileSASS = function (filename, options) {
    return sass('src/scss/*.scss', options)
        .pipe(autoprefixer('last 2 versions', '> 5%'))
        .pipe(concat(filename))
        .pipe(gulp.dest(DEST + '/css'))
        .pipe(browserSync.stream());
};

gulp.task('sass', function () {
    return compileSASS('custom.css', {});
});

gulp.task('sass-minify', function () {
    return compileSASS('custom.min.css', { style: 'compressed' });
});

gulp.task('browser-sync', function () {
    browserSync.init({
        server: {
            baseDir: './'
        },
        startPath: './production/index.html'
    });
});

gulp.task('watch', function () {
    // Watch .html files
    gulp.watch('production/*.html', browserSync.reload);
    // Watch .js files
    gulp.watch('src/js/*.js', ['scripts']);
    // Watch .scss files
    gulp.watch('src/scss/*.scss', ['sass', 'sass-minify']);
});