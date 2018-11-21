module.exports = function (grunt) {
    grunt.initConfig({
        concat: {
            all: {
                src: ['node_modules/jquery/dist/jquery.min.js', 'node_modules/popper.js/dist/popper.min.js','wwwroot/lib/temp/extra.min.js'],
                dest: 'wwwroot/lib/jsMain/combined.min.js'
            }
        },
        uglify: {
            all: {
                src: ['wwwroot/lib/jquery-validation/dist/additional-methods.js', 'wwwroot/lib/jquery-validation/dist/jquery.validate.js',
                    'wwwroot/lib/jquery-validation-unoobtrusive/jquery.validate.unobtrusive.js'],
                dest: 'wwwroot/lib/temp/extra.min.js'
            }
        },
        sass: {
            dist: {
                files: {
                    'wwwroot/lib/cssMain/bootstrap.css': 'node_modules/bootstrap/scss/custom.scss'
                }
            }
        },
        cssmin: {
            target: {
                files: {
                    'wwwroot/lib/cssMain/bootstrap.min.css': 'wwwroot/lib/cssMain/bootstrap.css'
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-sass');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
};