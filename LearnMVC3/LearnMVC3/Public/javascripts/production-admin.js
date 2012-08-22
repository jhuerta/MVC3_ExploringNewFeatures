Production = Backbone.Model.extend({
    idAttribute: "ID",
    url: function () {
        return this.isNew() ? "/learnmvc3/api/productions/create" : "/learnmvc3/api/productions/edit/" + this.get("ID");
//        return this.isNew() ? "/api/productions/create" : "/api/productions/edit/" + this.get("ID");
    },
    validate : function(atts) {
        if("Title" in atts & !atts.Title){
            return "Title is required!"
        }
    }
});
Productions = Backbone.Collection.extend({
    model: Production,
    url : "/learnmvc3/api/productions"
//    url : "/api/productions"
});

productions = new Productions();
model = new Production();

FormView = Backbone.View.extend({
    initialize: function () {
        _.bindAll(this, 'render');
        this.template = $("#productionFormTemplate");
        this.model.bind("reset", this.render);
    },
    events: {
        "change input": "updateModel",
        "keypress  input": "updateModel",
        "submit #productionForm": "save",
    },
    save: function () {
        this.model.save(
            this.model.attributes,
            {
                success: function (model,response  ) {
//                    alert(model.get("Title") + " saved");
                    notifierView.className = "success";
                    notifierView.message = model.get("Title") + " saved";
                    notifierView.render();
                },
                error: function (model, response) {
//                    alert("Uh oh!!!! ..." + model.get("Title") + " NOT saved");
                    notifierView.className = "error";
                    notifierView.message = "ERROR SAVING : " + response.responseText;
                    notifierView.render();
                }
            }
        );
        return false;
    },
    updateModel: function (evt) {
        var field = $(evt.currentTarget);
        var data = {};
        data[field.attr('ID')] = field.val();
        this.model.set(data);
    },
    render: function () {
        var html = this.template.tmpl(this.model.toJSON());
        $(this.el).html(html);
        $("#OldPrice").datepicker();
        return this;
    }

});

NotifierView = Backbone.View.extend({
        initialize: function () {
            this.template = $("#notifierTemplate");
            this.className = "Success";
            this.message = "Success";
    },
    render: function () {
        var html = this.template.tmpl( { message: this.message,className: this.className});
        $(this.el).html(html);
        return this;
    }
});
    

ListView = Backbone.View.extend({
    initialize: function () {
        _.bindAll(this, 'render');
        this.template = $("#listTemplate");
        this.collection.bind("reset", this.render);
    },
    events: {
        "click #new-production": "showCreate",
        "click #edit-form": "showForm"
    },
    showCreate: function () {
        app.navigate("create", true);
        return false;
    },
    showForm: function (evt) {
        var id = $(evt.currentTarget).data('production-id');
        app.navigate("edit/" + id, true);
        return false;
    },
    render: function () {
        var data = { items: this.collection.toJSON() }
        var html = this.template.tmpl(data);
        $(this.el).html(html);
        return this;
    }

});

jQuery(function () {
    productions.fetch({
        success: function () {
            window.app = new ProductionAdmin();
            Backbone.history.start();
        },
        error: function () {
            alert("error in routing");
        }
    });
});


var ProductionAdmin = Backbone.Router.extend({
    initialize: function () {
        notifierView = new NotifierView({ el: "#notifier" });
        listView = new ListView({ collection: productions, el: "#production-list" });
        formView = new FormView({ model: model, el: "#production-form" });
    },
    routes: {
        "": "index",
        "edit/:id": "edit",
        "create": "create"

    },
    index: function () {
        listView.render();
    },
    edit: function (id) {
        listView.render();
        $(formView.el).empty();
        var model = productions.get(id);
        formView.model = model;
        formView.render();
    },
    create: function () {
        var model = new Production();
        listView.render();
        $(formView.el).empty();
        formView.model = model;
        formView.render();
    }
});