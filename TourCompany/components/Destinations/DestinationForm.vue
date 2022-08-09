<template>
    <v-card>
        <v-card-title>
            <span class="text-h5">{{ formTitle }}</span>
        </v-card-title>

        <v-card-text>
            <v-container>
                <v-form 
                ref="form" 
                lazy-validation>
                    <v-text-field
                        :value="name"
                        label="Name"
                        :rules="nameRules"
                        @input="$emit('update:name', $event)"
                    >
                    </v-text-field>
                    <v-text-field
                        :value="price"
                        label="Price"
                        type="number"
                        prefix="₺"
                        :rules="priceRules"
                        @input="$emit('update:price', $event)"
                    >
                    </v-text-field>
                </v-form>
            </v-container>
        </v-card-text>

        <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="warning" text @click="$emit('on-close')">
                Cancel
            </v-btn>
            <v-btn color="primary" text @click="validate(); $emit('on-save', valid)">
                Save
            </v-btn>
        </v-card-actions>
    </v-card>
</template>
<script>

export default {
    props: {
        formTitle: {
            type: String,
            default: "Edit"
        },
        name: {
            type: String,
            default: ""
        },
        price: {
            type: Number,
            default: 20
        }
    },
    emits: ['onSave', 'onClose', 'update:name', 'update:price'],
    data() {
        return {
            valid: false,
            nameRules: [v => !!v || 'Name is required.'],
            priceRules: [
                    v => !!v || 'Price is required.',
                    v => (!!v && (v[0] !== '0' || (v.length > 1 && v[1] !== ','))) || 'Delete leading \'0\' s.',
                    v => (parseInt(v) >= 20) || 'Price must be bigger than 20.'
                ],
        }
    },
    methods: {
        validate () {
            this.valid = this.$refs.form.validate()
        },
    }
};
</script>